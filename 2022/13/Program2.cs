
using AdventOfCode2022;

var lines = File.ReadAllText("Input.txt");

var p = lines.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries).Select(_ => Conv(_)).ToList();
p.Insert(0, Conv("[[2]]"));
p.Insert(1, Conv("[[6]]"));

var s = p.OrderBy(x => x, new MyComparer());

var sIndexd = s.Select((_, i) => new { index = i, item = _.ToString() });

int m1 = sIndexd.First(_ => _.item == "[[2]]").index;
int m2 = sIndexd.First(_ => _.item == "[[6]]").index;

Console.WriteLine(m1 * m2);

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