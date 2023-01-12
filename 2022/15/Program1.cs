var lines = File.ReadAllLines("Input.txt");

var l1 = lines.Select(_ =>
{
    var parts = _.Split('=', ',', ':');
    return ((int sX, int sY, int bX, int bY))(int.Parse(parts[1]), int.Parse(parts[3]), int.Parse(parts[5]), int.Parse(parts[7]));
}).ToArray();

var l2 = l1.Select(_ =>
{
    var dist = GetDistance(_.sX, _.sY, _.bX, _.bY);
    return ((int sX, int sY, int bX, int bY, int dist))(_.sX, _.sY, _.bX, _.bY, dist);
}).ToArray();

var taken = new List<(int x, int y)>();
var targetY = 2000000;

foreach (var s in l2)
{
    // to right
    var x = s.sX;
    while (GetDistance(s.sX, s.sY, x, targetY) <= s.dist)
    {
        taken.Add((x, targetY));
        x++;
    }

    // to left
    x = s.sX - 1;
    while (GetDistance(s.sX, s.sY, x, targetY) <= s.dist)
    {
        taken.Add((x, targetY));
        x--;
    }
}

taken = taken.Distinct().ToList();
var xs = taken.Select(_ => _.x).OrderBy(_ => _).ToArray();

var bOnLineY = l2.Select(_ => (_.bX, _.bY)).Distinct().Count(_ => taken.Contains((_.bX, _.bY)));


Console.WriteLine(xs.Length - bOnLineY);

int GetDistance(int x1, int y1, int x2, int y2)
{
    return Math.Abs(x1 - x2) + Math.Abs(y1 - y2);
}