var lines = File.ReadAllLines("Input.txt");

var games = lines.Select(_ =>
{
    var parts = _.Split(":");
    return ((int Id, string[] Bags))(int.Parse(parts[0].Split().Last()), parts.Last().Split(';'));
});

var sum = 0;

foreach (var game in games)
{
    var cubes = game.Bags.SelectMany(_ =>
    {
        var parts = _.Split(',');
        return parts.Select(p => ((int Count, string Color))(int.Parse(p.Trim().Split()[0]), p.Trim().Split()[1]));
    });

    if (cubes.Where(_ => _.Color == "red").Any(_ => _.Count > 12)
        || cubes.Where(_ => _.Color == "green").Any(_ => _.Count > 13)
        || cubes.Where(_ => _.Color == "blue").Any(_ => _.Count > 14))
    {
        continue;
    }

    sum += game.Id;
}

Console.WriteLine(sum);