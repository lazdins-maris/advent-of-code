var lines = File.ReadAllLines("Input.txt");
var points = lines.Select(_ =>
{
    var p = _.Split(',');
    return ((int x, int y, int z))(int.Parse(p[0]), int.Parse(p[1]), int.Parse(p[2]));
}).ToArray();

var maxX = points.Max(_ => _.x);
var maxY = points.Max(_ => _.y);
var maxZ = points.Max(_ => _.z);

var freeSides = 0;

// z
for (int y = 0; y <= maxY; y++)
{
    for (int x = 0; x <= maxX; x++)
    {
        var zs = points.Where(_ => _.x == x && _.y == y).Select(_ => _.z).Distinct().OrderBy(_ => _).ToArray();

        if (zs.Length > 0)
        {
            // Search for gaps
            freeSides += CountGaps(zs) + 2;
        }
    }
}

// x
for (int y = 0; y <= maxY; y++)
{
    for (int z = 0; z <= maxZ; z++)
    {
        var xs = points.Where(_ => _.z == z && _.y == y).Select(_ => _.x).Distinct().OrderBy(_ => _).ToArray();

        if (xs.Length > 0)
        {
            // Search for gaps
            freeSides += CountGaps(xs) + 2;
        }
    }
}

// y
for (int x = 0; x <= maxX; x++)
{
    for (int z = 0; z <= maxZ; z++)
    {
        var ys = points.Where(_ => _.z == z && _.x == x).Select(_ => _.y).Distinct().OrderBy(_ => _).ToArray();

        if (ys.Length > 0)
        {
            // Search for gaps
            freeSides += CountGaps(ys) + 2;
        }
    }
}

int CountGaps(int[] ys)
{
    var count = 0;

    for (int i = 1; i < ys.Length; i++)
    {
        if (ys[i - 1] + 1 != ys[i])
            count += 2;
    }

    return count;
}

Console.WriteLine(freeSides);