using AdventOfCode2022;
using System.Text;

var timer = System.Diagnostics.Stopwatch.StartNew();

Console.WriteLine("Very slow. 2h on my PC");

var lines = File.ReadAllLines("Input.txt");
var blizzards = new List<Bliz>();
var maxX = lines[0].Length - 3;
var maxY = lines.Length - 3;

for (int y = 1; y < maxY + 2; y++)
{
    for (int x = 1; x < maxX + 2; x++)
    {
        var dir = lines[y][x];
        if (dir != '.')
            blizzards.Add(new Bliz(x - 1, y - 1, dir, maxX, maxY));
    }
}

var minute = 0;
var paths = new List<(int x, int y)>() { (0, -1) };
var seen = new HashSet<string>();


// 260 is comming from first solution
for (int i = 0; i < 260; i++)
    blizzards.ForEach(_ => _.Move());
minute = 260;

paths = new List<(int x, int y)>() { (maxX, maxY + 1) };
seen.Clear();

// Backward
do
{
    blizzards.ForEach(_ => _.Move());
    minute++;
    if (minute % 10 == 0)
    {
        Console.WriteLine($"{minute} / {paths.Count} / {timer.Elapsed} / {seen.Count} - {paths.OrderBy(_ => _.x + _.y).First()}");
    }

    paths = GetNewPaths(blizzards, paths, seen);
    paths = paths.OrderBy(_ => _.x + _.y).Take(200).ToList();

} while (!paths.Contains((0, 0)));

Console.WriteLine(++minute);
blizzards.ForEach(_ => _.Move());

paths = new List<(int x, int y)>() { (0, -1) };
seen.Clear();

// Forward
do
{
    blizzards.ForEach(_ => _.Move());
    minute++;
    if (minute % 10 == 0)
    {
        Console.WriteLine($"{minute} / {paths.Count} / {timer.Elapsed} / {seen.Count} - {paths.OrderByDescending(_ => _.x + _.y).First()}");
    }

    paths = GetNewPaths(blizzards, paths, seen);
    paths = paths.OrderByDescending(_ => _.x + _.y).Take(200).ToList();

} while (!paths.Contains((maxX, maxY)));

Console.WriteLine(++minute);


timer.Stop();
Console.WriteLine(timer.Elapsed);

string GetMap(List<Bliz> blizzards, (int x, int y) path)
{
    var map = new StringBuilder();
    for (int y = 0; y <= maxY; y++)
    {
        for (int x = 0; x <= maxX; x++)
        {
            if (path.x == x && path.y == y)
                map.Append('E');
            else
            {
                var bs = blizzards.Where(_ => _.X == x && _.Y == y).ToArray();
                var count = bs.Length;
                map.Append(count == 0 ? '.' : count == 1 ? bs[0].Dir : count.ToString()[0]);
            }
        }
        map.Append(Environment.NewLine);
    }

    return map.ToString();
}

List<(int x, int y)> GetNewPaths(List<Bliz> blizzards, List<(int x, int y)> paths, HashSet<string> seen)
{
    var newPaths = new List<(int x, int y)>();
    foreach (var path in paths)
    {
        // Wait
        if (blizzards.All(_ => _.X != path.x || _.Y != path.y))
        {
            var map = GetMap(blizzards, path);
            if (!seen.Contains(map))
            {
                seen.Add(map);
                newPaths.Add(path);
            }
        }

        // Up
        if (path.y > 0 && blizzards.All(_ => _.X != path.x || _.Y != path.y - 1))
        {
            var newPath = (path.x, path.y - 1);
            var map = GetMap(blizzards, newPath);
            if (!seen.Contains(map))
            {
                seen.Add(map);
                newPaths.Add(newPath);
            }
        }

        // Right
        if (path.x < maxX && blizzards.All(_ => _.X != path.x + 1 || _.Y != path.y))
        {
            var newPath = (path.x + 1, path.y);
            var map = GetMap(blizzards, newPath);
            if (!seen.Contains(map))
            {
                seen.Add(map);
                newPaths.Add(newPath);
            }
        }

        // Down
        if (path.y < maxY && blizzards.All(_ => _.X != path.x || _.Y != path.y + 1))
        {
            var newPath = (path.x, path.y + 1);
            var map = GetMap(blizzards, newPath);
            if (!seen.Contains(map))
            {
                seen.Add(map);
                newPaths.Add(newPath);
            }
        }

        // Left
        if (path.x > 0 && blizzards.All(_ => _.X != path.x - 1 || _.Y != path.y))
        {
            var newPath = (path.x - 1, path.y);
            var map = GetMap(blizzards, newPath);
            if (!seen.Contains(map))
            {
                seen.Add(map);
                newPaths.Add(newPath);
            }
        }
    }

    return newPaths;
}