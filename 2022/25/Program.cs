using AdventOfCode2022;

var timer = System.Diagnostics.Stopwatch.StartNew();

var lines = File.ReadAllLines("Input.txt");

var sum = lines.Sum(_ => (long)Snafu.ToNumber(_));

Console.WriteLine(sum);
Console.WriteLine(Snafu.ToSnafu(sum));

timer.Stop();
Console.WriteLine(timer.Elapsed);