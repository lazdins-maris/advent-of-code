var lines = File.ReadAllLines("Input.txt");

var map = new List<string>();
map.Add(new string('.', lines[0].Length + 2));
map.AddRange(lines.Select(_ => "." + _ + "."));
map.Add(new string('.', lines[0].Length + 2));

var mapId = new (int Id, int Value)[map[0].Length, map.Count];
var idCounter = 1;

for (int row = 1; row < map.Count - 1; row++)
{
    for (int col = 1; col < map[row].Length - 1; col++)
    {
        if (!Char.IsDigit(map[row][col]))
        {
            continue;
        }

        var numberLength = GetNumberLength(map[row], col);
        var number = int.Parse(map[row].Substring(col, numberLength));

        for (int i = 0; i < numberLength; i++)
        {
            mapId[col + i, row] = (idCounter, number);
        }

        idCounter++;
        col += numberLength - 1;
    }
}

var sum = 0;

for (int row = 1; row < map.Count - 1; row++)
{
    for (int col = 1; col < map[row].Length - 1; col++)
    {
        if (map[row][col] != '*')
        {
            continue;
        }

        var numberIds = GetNumbers(mapId, col, row);

        if (numberIds.Length == 2)
        {
            sum += numberIds[0].Value * numberIds[1].Value;
        }
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

static (int Id, int Value)[] GetNumbers((int Id, int Value)[,] mapId, int x, int y)
{
    var numbers = new List<(int Id, int Value)>
    {
        mapId[x - 1, y - 1],
        mapId[x - 0, y - 1],
        mapId[x + 1, y - 1],
        mapId[x - 1, y],
        mapId[x - 0, y],
        mapId[x + 1, y],
        mapId[x - 1, y + 1],
        mapId[x - 0, y + 1],
        mapId[x + 1, y + 1]
    };

    return numbers.Where(_ => _.Id != 0).Distinct().ToArray();
}