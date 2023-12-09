var lines = File.ReadAllLines("Input.txt");

var sum = lines.Sum(line => GetHistory(line.Split().Select(long.Parse).ToList()));

Console.WriteLine(sum);

long GetHistory(List<long> numbers)
{
    var history = new Stack<List<long>>();
    history.Push(numbers);

    var lastRow = numbers;
    do
    {
        lastRow = GetNextHistory(lastRow);
        history.Push(lastRow);
    } while (!lastRow.All(_ => _ == 0));

    // Reverse
    long number = 0;
    do
    {
        lastRow = history.Pop();
        number = lastRow.First() - number;
    } while (history.Any());

    Console.WriteLine(number);
    return number;
}

List<long> GetNextHistory(List<long> numbers)
{
    var result = new List<long>();

    for (int i = 0; i < numbers.Count - 1; i++)
    {
        result.Add(numbers[i + 1] - numbers[i]);
    }

    return result;
}