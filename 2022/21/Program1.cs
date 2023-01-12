using System.Diagnostics;

var timer = Stopwatch.StartNew();

var lines = File.ReadAllLines("Input.txt");
var ds = lines.Select(_ => _.Split(": ")).ToDictionary(_ => _[0], _ => _[1]);

do
{
    var keys = ds.Keys.ToArray();
    foreach (var key in keys)
    {
        if (ds[key].All(_ => char.IsDigit(_)))
        {
            foreach (var d in ds)
            {
                ds[d.Key] = d.Value.Replace(key, ds[key]);
            }
            ds.Remove(key);
        }
        else if (!ds[key].Any(_ => char.IsLetter(_)))
        {
            var parts = ds[key].Split(' ');
            var n1 = long.Parse(parts[0]);
            var n2 = long.Parse(parts[2]);
            var value = parts[1] == "+"
                ? n1 + n2
                : parts[1] == "-"
                    ? n1 - n2
                    : parts[1] == "*"
                        ? n1 * n2
                        : parts[1] == "/"
                            ? n1 / n2
                            : throw new Exception("Unknown operator: " + parts[1]);

            ds[key] = value.ToString();
        }
    }

} while (ds.Count > 1);

Console.WriteLine(ds.First());

timer.Stop();
Console.WriteLine(timer.Elapsed);
