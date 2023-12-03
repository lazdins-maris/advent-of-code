var lines = File.ReadAllLines("Input.txt");

var map = new List<string>();
map.Add(new string('.', lines[0].Length + 2));
map.AddRange(lines.Select(_ => "." + _ + "."));
map.Add(new string('.', lines[0].Length + 2));

var sum = 0;

for (int row = 1; row < map.Count - 1; row++)
{
    for (int col = 1; col < map[row].Length - 1; col++)
    {
        if (!Char.IsDigit(map[row][col]))
        {
            continue;
        }

        var numberLength = GetNumberLength(map[row], col);
        var expectedDotCount = (numberLength + 2) * 2 + 2;

        if (CountDots(map, col - 1, row - 1, numberLength + 2) != expectedDotCount)
        {
            sum += int.Parse(map[row].Substring(col, numberLength));
        }

        col += numberLength - 1;
    }
}

Console.WriteLine(sum);

static int GetNumberLength(string row, int col)
{
    var length = 1;

    while (Char.IsDigit(row[col + length]))
    {
        length++;
    }

    return length;
}

static int CountDots(List<string> map, int x, int y, int width) =>
    map[y].Substring(x, width).Count(_ => _ == '.')
    + map[y + 1].Substring(x, width).Count(_ => _ == '.')
    + map[y + 2].Substring(x, width).Count(_ => _ == '.');