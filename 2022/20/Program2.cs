using AdventOfCode2022;
using System.Diagnostics;

var timer = Stopwatch.StartNew();

var lines = File.ReadAllLines("Input.txt");
var ns = lines.Select(_ => new Num(long.Parse(_) * 811589153)).ToList();

var target = new List<Num>(ns);

for (int i = 0; i < 10; i++)
{
    Sorter.Sort2(ns, target);
}

var indexOf0 = target.IndexOf(ns.First(_ => _.N == 0));
var i1000th = (indexOf0 + 1000) % ns.Count;
var i2000th = (indexOf0 + 2000) % ns.Count;
var i3000th = (indexOf0 + 3000) % ns.Count;

Console.WriteLine(target[i1000th].N + target[i2000th].N + target[i3000th].N);

timer.Stop();
Console.WriteLine(timer.Elapsed);