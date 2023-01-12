var timer = System.Diagnostics.Stopwatch.StartNew();

var lines = File.ReadAllLines("Input.txt");
var points = new List<(int x, int y)>();
for (int y = 0; y < lines.Length; y++)
{
    for (int x = 0; x < lines[0].Length; x++)
    {
        if (lines[y][x] == '#')
            points.Add((x, y));
    }
}

var directionToCheck = new[] { 'N', 'S', 'W', 'E' };
var directionIndex = 0;
var round = 1;

var proposals = new Dictionary<(int x, int y), (int x, int y)>();

do
{
    Console.WriteLine($"== Round {round} ==");

    proposals = new Dictionary<(int x, int y), (int x, int y)>();
    foreach (var point in points)
    {
        if (IsAllFree(points, point))
            continue;

        var proposal = GetProposedMove(points, point, directionToCheck, directionIndex);
        if (proposal != null)
            proposals.Add(point, proposal.Value);
    }

    // Get unique proposals and move
    var uniqueProposals = proposals.GroupBy(_ => _.Value).Where(_ => _.Count() == 1).ToDictionary(_ => _.First().Key, _ => _.Key);
    foreach (var item in uniqueProposals)
    {
        points.Remove(item.Key);
        points.Add(item.Value);
    }

    directionIndex = directionIndex == 3 ? 0 : directionIndex + 1;
    round++;
} while (proposals.Count > 0 && round <= 10);

(int x, int y)? GetProposedMove(List<(int x, int y)> points, (int x, int y) point, char[] directionToCheck, int directionIndex)
{
    for (int i = 0; i < 4; i++)
    {
        switch (directionToCheck[directionIndex])
        {
            case 'N':
                if (points.Any(_ =>
                    (_.x + 0, _.y + 1) == point ||
                    (_.x + 1, _.y + 1) == point ||
                    (_.x - 1, _.y + 1) == point))
                    break;
                return (point.x, point.y - 1);
            case 'S':
                if (points.Any(_ =>
                    (_.x + 0, _.y - 1) == point ||
                    (_.x + 1, _.y - 1) == point ||
                    (_.x - 1, _.y - 1) == point))
                    break;
                return (point.x, point.y + 1);
            case 'W':
                if (points.Any(_ =>
                    (_.x + 1, _.y + 0) == point ||
                    (_.x + 1, _.y - 1) == point ||
                    (_.x + 1, _.y + 1) == point))
                    break;
                return (point.x - 1, point.y);
            case 'E':
                if (points.Any(_ =>
                    (_.x - 1, _.y + 0) == point ||
                    (_.x - 1, _.y - 1) == point ||
                    (_.x - 1, _.y + 1) == point))
                    break;
                return (point.x + 1, point.y);
        }

        directionIndex = directionIndex == 3 ? 0 : directionIndex + 1;
    }

    return null;
}

bool IsAllFree(List<(int x, int y)> points, (int x, int y) point)
{
    return !points.Any(_ =>
        (_.x, _.y - 1) == point
        || (_.x, _.y + 1) == point
        || (_.x - 1, _.y) == point
        || (_.x + 1, _.y) == point
        || (_.x - 1, _.y - 1) == point
        || (_.x + 1, _.y + 1) == point
        || (_.x + 1, _.y - 1) == point
        || (_.x - 1, _.y + 1) == point
    );
}

var width = points.Max(_ => _.x) - points.Min(_ => _.x) + 1;
var height = points.Max(_ => _.y) - points.Min(_ => _.y) + 1;
Console.WriteLine(width * height - points.Count);

timer.Stop();
Console.WriteLine(timer.Elapsed);
