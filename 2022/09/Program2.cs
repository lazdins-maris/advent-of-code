var lines = File.ReadAllLines("Input.txt");
var actions = lines.Select(_ =>
{
    var p = _.Split(' ');
    return ((char c, int n))(p[0][0], int.Parse(p[1]));
});


var visited = new List<(int x, int y)>();

(int x, int y)[] h = new[] { (0, 0), (0, 0), (0, 0), (0, 0), (0, 0), (0, 0), (0, 0), (0, 0), (0, 0), (0, 0) };

visited.Add(h.Last());

foreach (var action in actions)
{
    int dX = action.c == 'R' ? 1 : action.c == 'L' ? -1 : 0;
    int dY = action.c == 'D' ? 1 : action.c == 'U' ? -1 : 0; ;

    for (int i = 0; i < action.n; i++)
    {
        Move(h, 0, dX, dY);
    }
}

void Move((int x, int y)[] h, int n, int dX, int dY)
{
    var current = h[n];
    h[n] = (current.x + dX, current.y + dY);

    if (n == 9)
    {
        visited.Add(h.Last());
        return;
    }

    var next = h[n + 1];

    if (Math.Abs(current.x - next.x) + Math.Abs(current.y - next.y) == 0)
    {
        return;
    }

    if (Math.Abs(h[n].x - next.x) + Math.Abs(h[n].y - next.y) == 1 && dX != 0 && dY != 0)
    {
        return;
    }

    if (Math.Abs(current.x - next.x) + Math.Abs(current.y - next.y) == 1 && dX != 0 && dY != 0)
    {
        Move(h, n + 1, dX, dY);
        return;
    }

    // Horizontal or vertical move and still next to the next
    if (Math.Abs(dX) + Math.Abs(dY) == 1 && Math.Abs(h[n].x - next.x) + Math.Abs(h[n].y - next.y) == 1)
    {
        return;
    }

    var newDX = h[n].x - next.x;
    var newDY = h[n].y - next.y;

    if (Math.Abs(newDX) == 1 && Math.Abs(newDY) == 1)
    {
        return;
    }

    var nDX = newDX < -1 ? -1 : newDX > 1 ? 1 : newDX;
    var nDY = newDY < -1 ? -1 : newDY > 1 ? 1 : newDY;

    if (nDX == 0 && nDY == 0)
        return;

    Move(h, n + 1, nDX, nDY);
}

Console.WriteLine(visited.Distinct().Count());