var map = File.ReadAllLines("Input.txt");

(int x, int y) start = (0, 0);
for (int i = 0; i < map.Length; i++)
{
    if (map[i].Contains('E'))
    {
        start = (map[i].IndexOf('E'), i);
    }

    map[i] = map[i].Replace('S', (char)('a' - 1)).Replace('E', (char)('z' + 1));
}

var visited = new List<(int x, int y)>() { start };
var current = new List<(int x, int y)>() { start };
int depth = 0;

do
{
    depth++;
    var newCurrent = new List<(int x, int y)>();

    foreach (var c in current)
    {
        var next = new List<(int x, int y)>();
        next.Add((c.x - 1, c.y));
        next.Add((c.x + 1, c.y));
        next.Add((c.x, c.y + 1));
        next.Add((c.x, c.y - 1));

        next = next
            .Where(_ => _.x >= 0 && _.x < map[0].Length && _.y >= 0 && _.y < map.Length)
            .Where(_ => !visited.Contains(_))
            .Where(_ => map[c.y][c.x] - map[_.y][_.x] <= 1)
            .ToList();

        newCurrent.AddRange(next);
    }

    current = newCurrent.Distinct().ToList();
}
while (!current.Any(_ => map[_.y][_.x] == 'a'));

Console.WriteLine(depth);