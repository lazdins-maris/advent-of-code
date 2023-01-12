var lines = File.ReadAllLines("Input.txt");

var x = 1;
var cycle = 0;

int? x20 = null;
int? x60 = null;
int? x100 = null;
int? x140 = null;
int? x180 = null;
int? x220 = null;

for (int i = 0; i < lines.Length; i++)
{
    var parts = lines[i].Split(' ');
    if (parts[0] == "noop")
    {
        cycle++;
    }
    else
    {
        cycle += 2;
    }

    if (cycle >= 20 && !x20.HasValue)
        x20 = 20 * x;
    if (cycle >= 60 && !x60.HasValue)
        x60 = 60 * x;
    if (cycle >= 100 && !x100.HasValue)
        x100 = 100 * x;
    if (cycle >= 140 && !x140.HasValue)
        x140 = 140 * x;
    if (cycle >= 180 && !x180.HasValue)
        x180 = 180 * x;
    if (cycle >= 220 && !x220.HasValue)
        x220 = 220 * x;

    if (parts[0] != "noop")
    {
        x += int.Parse(parts[1]);
    }
}

Console.WriteLine(x20 + x60 + x100 + x140 + x180 + x220);