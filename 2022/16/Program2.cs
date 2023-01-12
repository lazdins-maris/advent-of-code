using AdventOfCode2022;

Console.WriteLine(2775);
Console.WriteLine("This is only partially correct solution.");

var lines = File.ReadAllLines("Input.txt");

var nodes = lines.Select(_ =>
{
    var parts = _.Split(' ', '=', ';');
    var childs = _.Contains("valves") ? _.Split("to valves ").Last().Split(", ") : _.Split("to valve ").Last().Split(", ");
    return new Node(parts[1], int.Parse(parts[5]), childs);
}).ToArray();

var nodesWithFlow = nodes.Where(_ => _.Flow > 0).ToArray();
var paths = new Dictionary<(Node start, Node end), int>();

foreach (var node1 in nodesWithFlow)
{
    foreach (var node2 in nodesWithFlow)
    {
        if (node1 == node2)
            continue;

        paths.Add((node1, node2), GetPath(node1, node2, nodes));
    }
}

var startNode = nodes.First(_ => _.Name == "AA");

foreach (var node in nodesWithFlow)
{
    paths.Add((startNode, node), GetPath(startNode, node, nodes));
}

int GetPath(Node node1, Node node2, Node[] nodes)
{
    var nodeNames = nodes.ToDictionary(_ => _.Name, _ => _.Childs);

    var visited = new string[] { node1.Name };
    var steps = 0;
    var skip = 0;

    do
    {
        steps++;
        var newVisited = new List<string>(visited);
        foreach (var v in visited[skip..])
        {
            newVisited.AddRange(nodeNames[v]);
        }
        skip = visited[skip..].Length;
        visited = newVisited.Distinct().ToArray();
    } while (!visited.Contains(node2.Name));

    return steps;
}

int max = Move(startNode, startNode, nodesWithFlow, 26, 26, 0, 0);

int Move(Node nodeMe, Node nodeEl, Node[] canOpen, int timeLeftMe, int timeLeftEl, int currentFlowMe, int currentFlowEl)
{
    var max = currentFlowMe + currentFlowEl;

    if (timeLeftMe == timeLeftEl)
    {
        var combos = new List<(Node me, Node el)>();

        foreach (var me in canOpen)
        {
            foreach (var el in canOpen)
            {
                if (me != el)
                    combos.Add((me, el));
            }
        }

        foreach (var combo in combos)
        {
            if (currentFlowMe == 0)
                Console.WriteLine(combo);

            var travelTimeMe = paths[(nodeMe, combo.me)];
            var newTimeLeftMe = timeLeftMe - (travelTimeMe + 1);

            var travelTimeEl = paths[(nodeEl, combo.el)];
            var newTimeLeftEl = timeLeftEl - (travelTimeEl + 1);

            if (newTimeLeftMe <= 0 && newTimeLeftEl <= 0)
                continue;

            var newCanOpen = new List<Node>(canOpen);

            var flowMe = currentFlowMe;
            if (newTimeLeftMe > 0)
            {
                flowMe += combo.me.Flow * newTimeLeftMe;
                newCanOpen.Remove(combo.me);
            }

            var flowEl = currentFlowEl;
            if (newTimeLeftEl > 0)
            {
                flowEl += combo.el.Flow * newTimeLeftEl;
                newCanOpen.Remove(combo.el);
            }

            var rez = Move(combo.me, combo.el, newCanOpen.ToArray(), newTimeLeftMe, newTimeLeftEl, flowMe, flowEl);

            if (2782 == rez)
            {
                Console.WriteLine($"My time: {timeLeftMe} --> {newTimeLeftMe} | El time: {timeLeftEl} --> {newTimeLeftEl} | NodeMe: {combo.me} | Node El: {combo.el} | FlowMe: {flowMe} | FlowEl: {flowEl}");
            }

            max = Math.Max(max, rez);
        }
    }
    else if (timeLeftMe > timeLeftEl)
    {
        // Only I pick next node
        foreach (var me in canOpen)
        {
            var travelTimeMe = paths[(nodeMe, me)];
            var newTimeLeftMe = timeLeftMe - (travelTimeMe + 1);

            if (newTimeLeftMe <= 0)
                continue;

            var newCanOpen = new List<Node>(canOpen);

            var flowMe = currentFlowMe;
            var timeAdjusted = Math.Abs(timeLeftMe - timeLeftEl) == 1 ? newTimeLeftMe : newTimeLeftMe;
            flowMe += me.Flow * timeAdjusted;
            newCanOpen.Remove(me);

            var rez = Move(me, nodeEl, newCanOpen.ToArray(), newTimeLeftMe, timeLeftEl, flowMe, currentFlowEl);

            if (2782 == rez)
            {
                Console.WriteLine($"My time: {timeLeftMe} --> {newTimeLeftMe} | NodeMe: {me} | FlowMe: {flowMe}");
            }

            max = Math.Max(max, rez);
        }
    }
    else
    {

        if (currentFlowMe == 1249 && currentFlowEl == 1513)
        {
            Console.WriteLine("----------");
            Console.WriteLine(String.Join(", ", canOpen.Select(_ => (paths[(nodeEl, _)], _))));
            Console.WriteLine("----------");
        }

        // Only Elephant pick next node
        foreach (var el in canOpen)
        {
            var travelTimeEl = paths[(nodeEl, el)];
            var newTimeLeftEl = timeLeftEl - (travelTimeEl + 1);

            if (newTimeLeftEl <= 0)
                continue;

            var newCanOpen = new List<Node>(canOpen);

            var flowEl = currentFlowEl;
            var timeAdjusted = Math.Abs(timeLeftMe - timeLeftEl) == 1 ? newTimeLeftEl : newTimeLeftEl;
            flowEl += el.Flow * timeAdjusted;
            newCanOpen.Remove(el);

            var rez = Move(nodeMe, el, newCanOpen.ToArray(), timeLeftMe, newTimeLeftEl, currentFlowMe, flowEl);

            if (2782 == rez)
            {
                Console.WriteLine($"El time: {timeLeftEl} --> {newTimeLeftEl} | NodeMe: {el} | FlowEl: {flowEl}");
            }

            max = Math.Max(max, rez);
        }
    }

    return max;
}

Console.WriteLine(max);