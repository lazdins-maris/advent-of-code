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

    var maxRed = cubes.Where(_ => _.Color == "red").Max(_ => _.Count);
    var maxGreen = cubes.Where(_ => _.Color == "green").Max(_ => _.Count);
    var maxBlue = cubes.Where(_ => _.Color == "blue").Max(_ => _.Count);

    sum += maxRed * maxGreen * maxBlue;
}

Console.WriteLine(sum);