var lines = File.ReadAllLines("Input.txt");

var sum = 0;

foreach (var line in lines)
{
    var digits = line
        .Replace("one", "o1e")
        .Replace("two", "t2o")
        .Replace("three", "t3e")
        .Replace("four", "f4r")
        .Replace("five", "f5e")
        .Replace("six", "s6x")
        .Replace("seven", "s7n")
        .Replace("eight", "e8t")
        .Replace("nine", "n9e")
        .Where(_ => Char.IsDigit(_))
        .ToArray();

    sum += int.Parse(digits[0].ToString() + digits.Last());
}

Console.WriteLine(sum);