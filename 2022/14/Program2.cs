var lines = File.ReadAllLines("Input.txt");

var linePoints = lines.Select(_ =>
{
    var points = _.Split(" -> ");
    return points.Select(p =>
    {
        var parts = p.Split(',');
        return ((int x, int y))(int.Parse(parts[0]), int.Parse(parts[1]));
    }).ToArray();
}).ToList();
linePoints.Add(new (int x, int y)[] { (329, 169), (671, 169) });

var minX = linePoints.SelectMany(_ => _).Min(_ => _.x);
var maxX = linePoints.SelectMany(_ => _).Max(_ => _.x);
var maxY = linePoints.SelectMany(_ => _).Max(_ => _.y);

var map = new char[maxX - minX + 1, maxY - 0 + 1];

foreach (var points in linePoints)
{
    for (int i = 1; i < points.Length; i++)
    {
        var prev = points[i - 1];
        var cur = points[i];

        if (prev.x == cur.x)
        {
            // Draw Vertical
            if (prev.y > cur.y)
            {
                var t = cur;
                cur = prev;
                prev = t;
            }

            for (int y = prev.y; y <= cur.y; y++)
            {
                map[cur.x - minX, y] = '#';
            }
        }
        else
        {
            // Horizontal
            if (prev.x > cur.x)
            {
                var t = cur;
                cur = prev;
                prev = t;
            }

            for (int x = prev.x; x <= cur.x; x++)
            {
                map[x - minX, cur.y] = '#';
            }
        }
    }
}

int count = 0;

try
{
    do
    {
        Drop(map, 500 - minX, 0);
        count++;
    }
    while (true);
}
catch (OutOfMemoryException)
{
    Console.WriteLine("Count: " + count);
}

void Drop(char[,] map, int x, int y)
{
    if (map[x, y] == 'o')
        // Reached full
        throw new OutOfMemoryException();

    if (map[x, y] != 0)
        throw new Exception();

    if (map[x, y + 1] == 0)
    {
        Drop(map, x, y + 1);
        return;
    }
    else if (map[x - 1, y + 1] == 0)
    {
        Drop(map, x - 1, y + 1);
        return;
    }
    else if (map[x + 1, y + 1] == 0)
    {
        Drop(map, x + 1, y + 1);
        return;
    }

    // Fill this spot
    map[x, y] = 'o';
}