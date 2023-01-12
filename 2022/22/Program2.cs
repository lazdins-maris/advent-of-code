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

// This is a config created for my specifig shape of cube layout
var sideMapping = new List<(int x1, int x2, int y1, int y2, int facing, Func<int, int, int> convertX, Func<int, int, int> convertY, int newFacing)>()
{
    // B' -> C
    (100, 100, 50, 99, 0, (x, y) => 100 + (y - 50), (x, y) => 49, 3),
    // C -> B'
    (100, 149, 50, 50, 1, (x, y) => 99, (x, y) => 50 + (x - 100), 2),
    // A' -> C
    (100, 100, 100, 149, 0, (x, y) => 149, (x, y) => 49 - (y - 100), 2),
    // C -> A'
    (150, 150, 0, 49, 0, (x, y) => 99, (x, y) => 100 + (49 - y), 2),
    // B -> A'
    (50, 50, 150, 199, 0, (x, y) => 50 + (y - 150), (x, y) => 149, 3),
    // A' -> B
    (50, 99, 150, 150, 1, (x, y) => 49, (x, y) => 150 + (x - 50), 2),
    // C' -> B'
    (0, 49, 99, 99, 3, (x, y) => 50, (x, y) => 50 + x, 0),
    // B' -> C'
    (49, 49, 50, 99, 2, (x, y) => y - 50, (x, y) => 100, 1),
    // C' -> A
    (-1, -1, 100, 149, 2, (x, y) => 50, (x, y) => 49 - (y - 100), 0),
    // A -> C'
    (49, 49, 0, 49, 2, (x, y) => 0, (x, y) => 100 + (49 - y), 0),
    // A -> B
    (50, 99, -1, -1, 3, (x, y) => 0, (x, y) => 150 + (x - 50), 0),
    // B -> A
    (-1, -1, 150, 199, 2, (x, y) => 50 + (y - 150), (x, y) => 0, 1),
    // C -> B
    (100, 149, -1, -1, 3, (x, y) => x - 100, (x, y) => 199, 3),
    // B -> C
    (0, 49, 200, 200, 1, (x, y) => 100 + x, (x, y) => 0, 1),

    // For other cases
    (0, cols, 0, rows, 0, (x, y) => x, (x, y) => y, 0),
    (0, cols, 0, rows, 1, (x, y) => x, (x, y) => y, 1),
    (0, cols, 0, rows, 2, (x, y) => x, (x, y) => y, 2),
    (0, cols, 0, rows, 3, (x, y) => x, (x, y) => y, 3),
};

(int x, int y) poz = (lines[0].Count(_ => _ == ' '), 0);
foreach (var com in coms)
{
    var c = com;
    facing = getNewFacing(facing, c.dir);
    // Move com.dist
    (int x, int y, int facing, bool isWall) next = (0, 0, 0, false);

    while (c.dist > 0 && !next.isWall)
    {
        switch (facing)
        {
            case 0:
                next = NextToRight(poz, facing);
                break;
            case 2:
                next = NextToLeft(poz, facing);
                break;
            case 1:
                next = NextToDown(poz, facing);
                break;
            case 3:
                next = NextToTop(poz, facing);
                break;
        }

        c = (c.dir, c.dist - 1);
        if (!next.isWall)
        {
            poz = (next.x, next.y);
            facing = next.facing;
        }
    }

    if (map[poz.x, poz.y] == null)
        throw new Exception(poz.ToString());
}

(int x, int y, int facing, bool isWall) NextToRight((int x, int y) poz, int facing)
{
    int x = poz.x + 1;
    int y = poz.y;
    var sMapping = sideMapping.First(_ => _.x1 <= x && x <= _.x2 && _.y1 <= y && y <= _.y2 && _.facing == facing);

    x = sMapping.convertX(poz.x + 1, poz.y);
    y = sMapping.convertY(poz.x + 1, poz.y);
    facing = sMapping.newFacing;

    var isWall = map[x, y].Value;
    return (x, y, facing, isWall);
}

(int x, int y, int facing, bool isWall) NextToLeft((int x, int y) poz, int facing)
{
    int x = poz.x - 1;
    int y = poz.y;
    var sMapping = sideMapping.First(_ => _.x1 <= x && x <= _.x2 && _.y1 <= y && y <= _.y2 && _.facing == facing);

    x = sMapping.convertX(poz.x - 1, poz.y);
    y = sMapping.convertY(poz.x - 1, poz.y);
    facing = sMapping.newFacing;

    var isWall = map[x, y].Value;
    return (x, y, facing, isWall);
}

(int x, int y, int facing, bool isWall) NextToDown((int x, int y) poz, int facing)
{
    int x = poz.x;
    int y = poz.y + 1;
    var sMapping = sideMapping.First(_ => _.x1 <= x && x <= _.x2 && _.y1 <= y && y <= _.y2 && _.facing == facing);

    x = sMapping.convertX(poz.x, poz.y + 1);
    y = sMapping.convertY(poz.x, poz.y + 1);
    facing = sMapping.newFacing;

    var isWall = map[x, y].Value;
    return (x, y, facing, isWall);
}

(int x, int y, int facing, bool isWall) NextToTop((int x, int y) poz, int facing)
{
    int x = poz.x;
    int y = poz.y - 1;
    var sMapping = sideMapping.First(_ => _.x1 <= x && x <= _.x2 && _.y1 <= y && y <= _.y2 && _.facing == facing);

    x = sMapping.convertX(poz.x, poz.y - 1);
    y = sMapping.convertY(poz.x, poz.y - 1);
    facing = sMapping.newFacing;

    var isWall = map[x, y].Value;
    return (x, y, facing, isWall);
}

Console.WriteLine((poz.y + 1) * 1000 + (poz.x + 1) * 4 + facing);

timer.Stop();
Console.WriteLine(timer.Elapsed);