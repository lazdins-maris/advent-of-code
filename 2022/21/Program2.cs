var timer = System.Diagnostics.Stopwatch.StartNew();

var lines = File.ReadAllLines("Input.txt");
var ds = lines.Select(_ => _.Split(": ")).ToDictionary(_ => _[0], _ => _[1]);

ds["humn"] = "not existing key";
bool hasWork;

do
{
    hasWork = false;
    var keys = ds.Keys.ToArray();
    foreach (var key in keys)
    {
        if (ds[key].All(_ => char.IsDigit(_)) || ds[key][0] == '-')
        {
            hasWork = true;

            foreach (var d in ds)
            {
                ds[d.Key] = d.Value.Replace(key, ds[key]);
            }
            ds.Remove(key);
        }
        else if (!ds[key].Any(_ => char.IsLetter(_)))
        {
            hasWork = true;

            var parts = ds[key].Split(' ');
            var n1 = long.Parse(parts[0]);
            var n2 = long.Parse(parts[2]);
            var value = parts[1] == "+"
                ? n1 + n2
                : parts[1] == "-"
                    ? n1 - n2
                    : parts[1] == "*"
                        ? n1 * n2
                        : n1 / n2;

            ds[key] = value.ToString();
        }
    }

    if (!hasWork)
    {
        break;
    }

} while (hasWork);

var root = ds["root"].Split();
var currentKey = root[0];
var expectedNumber = long.Parse(root[2]);

do
{
    var newRoot = ds[currentKey].Split();
    var isNumberFirst = char.IsDigit(newRoot[0][0]);
    var newKey = isNumberFirst ? newRoot[2] : newRoot[0];
    var number = long.Parse(isNumberFirst ? newRoot[0] : newRoot[2]);

    expectedNumber = newRoot[1] == "+"
        ? expectedNumber - number
        : newRoot[1] == "-"
            ? (isNumberFirst ? number - expectedNumber : expectedNumber + number)
            : newRoot[1] == "*"
                ? expectedNumber / number
                : expectedNumber * number;

    ds.Remove(currentKey);
    currentKey = newKey;

} while (ds.Count > 1 && currentKey != "humn");

Console.WriteLine(expectedNumber);

timer.Stop();
Console.WriteLine(timer.Elapsed);