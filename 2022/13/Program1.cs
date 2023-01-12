
using AdventOfCode2022;

var lines = File.ReadAllText("Input.txt");

var linePairs = lines.Split($"{Environment.NewLine}{Environment.NewLine}");

var pairs = linePairs.Select(pair =>
{
    var parts = pair.Split(Environment.NewLine);
    return (Conv(parts[0]), Conv(parts[1]));
}).ToArray();

var indexSum = 0;
for (int i = 0; i < pairs.Length; i++)
{
    if (IsRightOrder(pairs[i].Item1, pairs[i].Item2) ?? true)
    {
        indexSum += (i + 1);
    }
}

bool? IsRightOrder(Item item1, Item item2)
{
    if (item1.Value.HasValue && item2.Value.HasValue)
    {
        if (item1.Value == item2.Value)
            return null;

        return item1.Value < item2.Value;
    }

    if (!item1.Value.HasValue && !item2.Value.HasValue)
    {
        bool? isRight = null;

        if (item1.Child.Length == 0 && item2.Child.Length > 0)
            return true;

        for (int i = 0; i < item1.Child.Length; i++)
        {
            if (item2.Child.Length <= i)
                return false;

            isRight = IsRightOrder(item1.Child[i], item2.Child[i]);

            if (isRight.HasValue)
                return isRight;
        }

        return isRight;
    }

    if (item1.Value.HasValue)
    {
        item1 = new Item(new[] { item1 });
    }
    if (item2.Value.HasValue)
    {
        item2 = new Item(new[] { item2 });
    }

    return IsRightOrder(item1, item2);
}

Console.WriteLine(indexSum);

Item Conv(string text)
{
    if (text == "")
        return new Item(new Item[0]);

    if (text[0] != '[')
        return new Item(int.Parse(text));

    int opened = 0;
    for (int i = 0; i < text.Length; i++)
    {
        if (text[i] == '[')
            opened++;

        if (text[i] == ']')
        {
            if (opened > 0)
                opened--;

            if (opened == 0)
            {
                // Reached end of current block
                var block = text.Substring(1, i - 1);
                var parts = Split(block);

                return new Item(parts.Select(_ => Conv(_)).ToArray());
            }
        }
    }

    throw new Exception($"No closing tag for: {text}");
}

static List<string> Split(string text)
{
    var result = new List<string>();

    if (text == "")
        return result;

    int current = 0;
    int opened = 0;

    for (int i = 0; i < text.Length; i++)
    {
        if ((text[i] == ',' && opened == 0) || i + 1 == text.Length)
        {
            if (i + 1 == text.Length)
                i++;

            result.Add(text.Substring(current, i - current));
            current = i + 1;
            continue;
        }

        if (text[i] == '[')
            opened++;

        if (text[i] == ']')
        {
            if (opened > 0)
                opened--;
        }
    }

    return result;
}