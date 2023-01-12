var lines = File.ReadAllLines("Input.txt");
var points = lines.Select(_ =>
{
    var p = _.Split(',');
    return ((int x, int y, int z))(int.Parse(p[0]), int.Parse(p[1]), int.Parse(p[2]));
}).ToArray();

var maxX = points.Max(_ => _.x);
var maxY = points.Max(_ => _.y);
var maxZ = points.Max(_ => _.z);

var map = new int[maxX + 1, maxY + 1, maxZ + 1]; // 0 - empty space, 1 - lawa, 2 - air pocket
foreach (var p in points)
{
    map[p.x, p.y, p.z] = 1;
}

for (int x = 1; x < maxX; x++)
{
    for (int y = 1; y < maxY; y++)
    {
        for (int z = 1; z < maxZ; z++)
        {
            if (map[x, y, z] == 0)
                CheckPocket(map, x, y, z);
        }
    }
}

void CheckPocket(int[,,] map, int x, int y, int z)
{
    var visited = new List<(int x, int y, int z)>() { (x, y, z) };

    do
    {
        var newMoves = new List<(int x, int y, int z)>();

        foreach (var v in visited)
        {
            var moves = new (int x, int y, int z)[]
            {
                (v.x + 1, v.y, v.z),
                (v.x - 1, v.y, v.z),
                (v.x, v.y + 1, v.z),
                (v.x, v.y - 1, v.z),
                (v.x, v.y, z + 1),
                (v.x, v.y, z - 1)
            };

            var validMoves = moves.Where(_ =>
                map[_.x, _.y, _.z] == 0
                && !visited.Contains((_.x, _.y, _.z))
                && !newMoves.Contains((_.x, _.y, _.z)))
                .ToArray();

            if (validMoves.Any(_ => _.x == 0 || _.x == maxX || _.y == 0 || _.y == maxY || _.z == 0 || _.z == maxZ))
            {
                // Reached outside
                return;
            }

            newMoves.AddRange(validMoves);
        }

        if (newMoves.Count > 0)
        {
            visited.AddRange(newMoves);
            visited = visited.Distinct().ToList();
        }
        else
        {
            // No where to go. All visited is in trap
            // Mark as trapped
            foreach (var trapped in visited)
            {
                map[trapped.x, trapped.y, trapped.z] = 2;
            }
            return;
        }

    } while (true);
}

int CountFreeSides((int x, int y, int z)[] points)
{
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

    return freeSides;
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

var trappedPoints = new List<(int x, int y, int z)>();
for (int x = 1; x < maxX; x++)
{
    for (int y = 1; y < maxY; y++)
    {
        for (int z = 1; z < maxZ; z++)
        {
            if (map[x, y, z] == 2)
                trappedPoints.Add((x, y, z));
        }
    }
}

int allFreeSides = CountFreeSides(points);
int trappedSides = CountFreeSides(trappedPoints.ToArray());
Console.WriteLine(allFreeSides - trappedSides);