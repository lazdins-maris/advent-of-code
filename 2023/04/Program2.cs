var lines = File.ReadAllLines("Input.txt");

var cards = lines.Select(line =>
{
    var parts = line.Split(':', '|');
    var winning = parts[1].Trim().Split(' ').Where(_ => _.Length > 0).Select(int.Parse).ToArray();
    var numbers = parts[2].Trim().Split(' ').Where(_ => _.Length > 0).Select(int.Parse).ToArray();
    return ((string Card, int Instances, int[] Winning, int[] Numbers))(parts[0], 1, winning, numbers);
}).ToArray();

for (int i = 0; i < cards.Length; i++)
{
    var count = 0;
    foreach (var number in cards[i].Numbers)
    {
        if (cards[i].Winning.Contains(number))
        {
            count++;
        }
    }

    for (int j = 0; j < count; j++)
    {
        cards[i + j + 1].Instances += cards[i].Instances;
    }
}

var sum = cards.Sum(_ => _.Instances);

Console.WriteLine(sum);