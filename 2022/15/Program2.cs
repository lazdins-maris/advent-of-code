using AdventOfCode2022;
using System.Collections;

// y = 2855041 AND 2911364, 4327534 -- -419935, 2911362
Console.WriteLine(((long)2911362 + 1) * 4_000_000 + 2855041);
return;

var lines = File.ReadAllLines("Input.txt");

var l1 = lines.Select(_ =>
{
    var parts = _.Split('=', ',', ':');
    return ((int sX, int sY, int bX, int bY))(int.Parse(parts[1]), int.Parse(parts[3]), int.Parse(parts[5]), int.Parse(parts[7]));
}).ToArray();

var l2 = l1.Select(_ =>
{
    var dist = GetDistance(_.sX, _.sY, _.bX, _.bY);
    return ((int sX, int sY, int bX, int bY, int dist))(_.sX, _.sY, _.bX, _.bY, dist);
}).ToArray();

var maxArea = 4_000_000;

for (int y = 0; y < maxArea; y++)
{
    var inte = new List<(int x1, int x2)>();
    foreach (var s in l2)
    {
        int dX = s.dist - Math.Abs(s.sY - y);
        if (dX >= 0)
        {
            inte.Add((s.sX - dX, s.sX + dX));
        }
    }

    MergeIntervals(y, inte);

    if (y != 0 && y % 10_000 == 0)
        Console.WriteLine($"{maxArea * 100 / y}%");
}

int GetDistance(int x1, int y1, int x2, int y2)
{
    return Math.Abs(x1 - x2) + Math.Abs(y1 - y2);
}

static void MergeIntervals(int y, List<(int x1, int x2)> arr2)
{
    var arr = arr2.ToArray();
    Array.Sort(arr, new SortHelper());

    Stack stack = new Stack();

    stack.Push(arr[0]);

    for (int i = 1; i < arr.Length; i++)
    {

        (int x1, int x2) top = ((int x1, int x2))stack.Peek();

        if (top.x2 < arr[i].x1)
            stack.Push(arr[i]);

        else if (top.x2 < arr[i].x2)
        {
            top.x2 = arr[i].x2;
            stack.Pop();
            stack.Push(top);
        }
    }

    if (stack.Count > 1)
    {
        var s1 = ((int x1, int x2))stack.Pop();
        var s2 = ((int x1, int x2))stack.Pop();
        Console.WriteLine($"y = {y} AND {s1.x1}, {s1.x2} -- {s2.x1}, {s2.x2}");
        throw new Exception();
    }
}