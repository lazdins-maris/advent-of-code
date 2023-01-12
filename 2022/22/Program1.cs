var timer = System.Diagnostics.Stopwatch.StartNew();

var lines = File.ReadAllLines("Input.txt").ToList();
var rows = lines.IndexOf("");
var cols = lines.Take(rows).Max(_ => _.Length);
var map = new bool?[cols, rows];
for (int y = 0; y < rows; y++)
{
    for (int x = 0; x < cols; x++)
    {
        map[x, y] = (lines[y].Length <= x || lines[y][x] == ' ') ? (bool?)null : lines[y][x] == '#';
    }
}

var commandLine = lines.Last();
var coms = new List<(char dir, int dist)>();
var lastCom = ((char dir, int dist))('R', 0);
var numberStart = 0;
for (int i = 0; i < commandLine.Length; i++)
{
    if (char.IsLetter(commandLine[i]))
    {
        // Add new command
        var dist = commandLine.Substring(numberStart, i - numberStart);
        coms.Add((lastCom.dir, int.Parse(dist)));

        numberStart = i + 1;
        lastCom = (commandLine[i], 0);
    }
}
var lastDist = commandLine.Substring(numberStart);
coms.Add((lastCom.dir, int.Parse(lastDist)));

// 0 for right (>), 1 for down (v), 2 for left (<), and 3 for up (^)
var facing = 3;
var getNewFacing = (int currentFacing, char dir) =>
{
    if (dir == 'R')
    {
        return currentFacing < 3 ? currentFacing + 1 : 0;
    }

    return currentFacing > 0 ? currentFacing - 1 : 3;
};

(int x, int y) poz = (lines[0].Count(_ => _ == ' '), 0);
foreach (var com in coms)
{
    var c = com;
    facing = getNewFacing(facing, c.dir);
    // Move com.dist
    switch (facing)
    {
        case 0:
            var next0 = NextToRight(poz);
            while (c.dist > 0 && !next0.isWall)
            {
                c = (c.dir, c.dist - 1);
                poz = (next0.x, poz.y);
                next0 = NextToRight(poz);
            }
            break;
        case 2:
            var next2 = NextToLeft(poz);
            while (c.dist > 0 && !next2.isWall)
            {
                c = (c.dir, c.dist - 1);
                poz = (next2.x, poz.y);
                next2 = NextToLeft(poz);
            }
            break;
        case 1:
            var next1 = NextToDown(poz);
            while (c.dist > 0 && !next1.isWall)
            {
                c = (c.dir, c.dist - 1);
                poz = (poz.x, next1.y);
                next1 = NextToDown(poz);
            }
            break;
        case 3:
            var next3 = NextToTop(poz);
            while (c.dist > 0 && !next3.isWall)
            {
                c = (c.dir, c.dist - 1);
                poz = (poz.x, next3.y);
                next3 = NextToTop(poz);
            }
            break;
        default:
            throw new Exception("Unknown facing: " + facing);
    }

    if (map[poz.x, poz.y] == null)
        throw new Exception(poz.ToString());
}

(bool isWall, int x) NextToRight((int x, int y) poz)
{
    var line = lines[poz.y];
    var indexOfNext = poz.x + 1;
    if (indexOfNext == line.Length)
    {
        var indexOfFirst = Math.Min(line.IndexOf('.'), line.IndexOf('#'));
        indexOfNext = indexOfFirst;
    }
    return (line[indexOfNext] == '#', indexOfNext);
}

(bool isWall, int x) NextToLeft((int x, int y) poz)
{
    var newX = poz.x - 1;
    while ((newX >= 0 && map[newX, poz.y] == null) || newX == -1)
    {
        if (newX == -1)
            newX = lines[poz.y].Length - 1;
        else
            newX--;
    }

    return (lines[poz.y][newX] == '#', newX);
}

(bool isWall, int y) NextToDown((int x, int y) poz)
{
    var newY = poz.y + 1;
    while ((newY < rows && map[poz.x, newY] == null) || newY == rows)
    {
        if (newY == rows)
            newY = 0;
        else
            newY++;
    }

    return (map[poz.x, newY].Value, newY);
}

(bool isWall, int y) NextToTop((int x, int y) poz)
{
    var newY = poz.y - 1;
    while ((newY >= 0 && map[poz.x, newY] == null) || newY == -1)
    {
        if (newY == -1)
            newY = rows - 1;
        else
            newY--;
    }

    return (map[poz.x, newY].Value, newY);
}

Console.WriteLine((poz.y + 1) * 1000 + (poz.x + 1) * 4 + facing);

timer.Stop();
Console.WriteLine(timer.Elapsed);