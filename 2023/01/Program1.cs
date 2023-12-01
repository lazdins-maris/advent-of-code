var lines = File.ReadAllLines("Input.txt");

var sum = 0;

foreach (var line in lines)
{
    var digits = line.Where(_ => Char.IsDigit(_)).ToArray();

    sum += int.Parse(digits[0].ToString() + digits.Last());
}

Console.WriteLine(sum);