var lines = File.ReadAllLines("Input.txt");

var cards = lines.Select(line =>
{
    var parts = line.Split(':', '|');
    var winning = parts[1].Trim().Split(' ').Where(_ => _.Length > 0).Select(int.Parse).ToArray();
    var numbers = parts[2].Trim().Split(' ').Where(_ => _.Length > 0).Select(int.Parse).ToArray();
    return ((string Card, int[] Winning, int[] Numbers))(parts[0], winning, numbers);
}).ToArray();

var sum = 0;

foreach (var card in cards)
{
    var cardScore = 0;
    foreach (var number in card.Numbers)
    {
        if (card.Winning.Contains(number))
        {
            cardScore = cardScore == 0 ? 1 : cardScore * 2;
        }
    }

    sum += cardScore;
}

Console.WriteLine(sum);