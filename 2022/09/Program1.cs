var lines = File.ReadAllLines("Input.txt");
var actions = lines.Select(_ =>
{
    var p = _.Split(' ');
    return ((char c, int n))(p[0][0], int.Parse(p[1]));
});

var visited = new List<(int x, int y)>();

(int x, int y) h = (0, 0);
(int x, int y) t = (0, 0);

visited.Add(t);

foreach (var action in actions)
{
    switch (action.c)
    {
        case 'R':
            {
                for (int i = 0; i < action.n; i++)
                {
                    h = (h.x + 1, h.y);
                    if (h.x - t.x > 1)
                    {
                        t = (t.x + 1, h.y);
                        visited.Add(t);
                    }
                }
                break;
            }

        case 'L':
            {
                for (int i = 0; i < action.n; i++)
                {
                    h = (h.x - 1, h.y);
                    if (t.x - h.x > 1)
                    {
                        t = (t.x - 1, h.y);
                        visited.Add(t);
                    }
                }
                break;
            }

        case 'D':
            {
                for (int i = 0; i < action.n; i++)
                {
                    h = (h.x, h.y + 1);
                    if (h.y - t.y > 1)
                    {
                        t = (h.x, t.y + 1);
                        visited.Add(t);
                    }
                }
                break;
            }

        case 'U':
            {
                for (int i = 0; i < action.n; i++)
                {
                    h = (h.x, h.y - 1);
                    if (t.y - h.y > 1)
                    {
                        t = (h.x, t.y - 1);
                        visited.Add(t);
                    }
                }
                break;
            }

        default:
            throw new InvalidOperationException(action.c.ToString());
    }
}

Console.WriteLine(visited.Distinct().Count());