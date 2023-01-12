var lines = File.ReadAllLines("Input.txt");

var x = 0;
var cycle = 0;
var crt = Enumerable.Repeat<char>(' ', 240).ToArray();


for (int i = 0; i < lines.Length; i++)
{
    var parts = lines[i].Split(' ');
    if (parts[0] == "noop")
    {
        Draw(crt, cycle, x);
        cycle++;
    }
    else
    {
        Draw(crt, cycle, x);
        cycle++;
        Draw(crt, cycle, x);
        cycle++;
    }

    if (parts[0] != "noop")
    {
        x += int.Parse(parts[1]);
    }
}

void Draw(char[] crt, int cycle, int x)
{
    crt[cycle] = (cycle % 40 == x || cycle % 40 == x + 1 || cycle % 40 == x + 2) ? '#' : '.';
}

Console.WriteLine(String.Concat(crt.Skip(0).Take(40)));
Console.WriteLine(String.Concat(crt.Skip(40).Take(40)));
Console.WriteLine(String.Concat(crt.Skip(80).Take(40)));
Console.WriteLine(String.Concat(crt.Skip(120).Take(40)));
Console.WriteLine(String.Concat(crt.Skip(160).Take(40)));
Console.WriteLine(String.Concat(crt.Skip(200).Take(40)));