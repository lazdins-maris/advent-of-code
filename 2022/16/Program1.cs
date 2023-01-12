using AdventOfCode2022;
using System.Xml.Linq;

var lines = File.ReadAllLines("Input.txt");

var nodes = lines.Select(_ =>
{
    var parts = _.Split(' ', '=', ';');
    var childs = _.Contains("valves") ? _.Split("to valves ").Last().Split(", ") : _.Split("to valve ").Last().Split(", ");
    return new Node(parts[1], int.Parse(parts[5]), childs);
}).ToArray();

var nodesWithWlow = nodes.Where(_ => _.Flow > 0).ToArray();
var paths = new Dictionary<(Node start, Node end), int>();

foreach (var node1 in nodesWithWlow)
{
    foreach (var node2 in nodesWithWlow)
    {
        if (node1 == node2)
            continue;

        paths.Add((node1, node2), GetPath(node1, node2, nodes));
    }
}

var startNode = nodes.First(_ => _.Name == "AA");

foreach (var node in nodesWithWlow)
{
    paths.Add((startNode, node), GetPath(startNode, node, nodes));
}

int GetPath(Node node1, Node node2, Node[] nodes)
{
    var nodeNames = nodes.ToDictionary(_ => _.Name, _ => _.Childs);

    var visited = new List<string>() { node1.Name };
    var steps = 0;

    do
    {
        steps++;
        var newVisited = new List<string>(visited);
        foreach (var v in visited)
        {
            newVisited.AddRange(nodeNames[v]);
        }
        visited = newVisited.Distinct().ToList();
    } while (!visited.Contains(node2.Name));

    return steps;
}

int max = Move(startNode, nodesWithWlow, 30, 0);

int Move(Node node, Node[] canOpen, int timeLeft, int currentFlow)
{
    var max = currentFlow;

    foreach (var nextNode in canOpen)
    {
        var travelTime = paths[(node, nextNode)];
        var newTimeLeft = timeLeft - (travelTime + 1);
        if (newTimeLeft <= 0)
            continue;

        var flow = nextNode.Flow * newTimeLeft;
        var newCanOpen = canOpen.Where(_ => _ != nextNode).ToArray();

        var rez = Move(nextNode, newCanOpen, newTimeLeft, currentFlow + flow);

        if (2247 == rez)
        {
            Console.WriteLine($"{timeLeft} --> {newTimeLeft}");
        }

        max = Math.Max(max, rez);
    }

    return max;
}

Console.WriteLine(max);