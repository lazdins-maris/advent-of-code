using AdventOfCode2022;

var lines = File.ReadAllLines("Input.txt");
var bs = lines.Take(3).Select(_ =>
{
    var p = _.Split(' ', ':');
    return new Blueprint(int.Parse(p[1]), int.Parse(p[7]), int.Parse(p[13]), (int.Parse(p[19]), int.Parse(p[22])), (int.Parse(p[28]), int.Parse(p[31])));
}).ToArray();

// One ore robot
// Ore robot to collect clay
// Clay to create obsidian-collecting robots
// obs robot to create geode-cracking robots

foreach (var b in bs)
{
    var memory = new Dictionary<(int time, int oreRobots, int clayRobots, int obsidianRobots, int geodeRobots, int ores, int clays, int obsidians, int geodeCracked), int>();
    Console.WriteLine($"B ID: {b.Id}");
    var time = 0;
    var state = new State { OreRobots = 1 };
    var maxOpenQeodes = Move(b, state, time, memory);
    b.SetQualityLevel(maxOpenQeodes);
    Console.WriteLine($"B ID: {b.Id} quality level = {maxOpenQeodes}");
}

int Move(Blueprint b, State state, int time, Dictionary<(int time, int oreRobots, int clayRobots, int obsidianRobots, int geodeRobots, int ores, int clays, int obsidians, int geodeCracked), int> memory)
{
    if (time == 32)
        return state.GeodeCracked;

    if (time == 5)
        Console.WriteLine(state);

    var memItem = (time, state.OreRobots, state.ClayRobots, state.ObsidianRobots, state.GeodeRobots, state.Ores, state.Clays, state.Obsidians, state.GeodeCracked);
    if (memory.ContainsKey(memItem))
        return memory[memItem];

    // get build actions (assume one robot per move - can be more of one type or different)
    var builds = new List<(int oreRobots, int clayRobots, int obsidianRobots, int geodeRobots)>();

    if (state.Ores / b.GeodeRobotCost.ore > 0 && state.Obsidians / b.GeodeRobotCost.obsidian > 0)
        builds.Add((0, 0, 0, 1));
    else if (state.Ores / b.ObsidianRobotCost.ore > 0 && state.Clays / b.ObsidianRobotCost.clay > 0)
    {
        builds.Add((0, 0, 1, 0));
        // If only one of thees can be built add option to wait
        if (Math.Min(state.Ores / b.ObsidianRobotCost.ore, state.Clays / b.ObsidianRobotCost.clay) == 1)
            builds.Add((0, 0, 0, 0));

        if (state.Ores / b.ClayRobotCost > 0)
            builds.Add((0, 0, 0, 0));

        if (state.Ores / b.OreRobotCost > 0)
            builds.Add((1, 0, 0, 0));
    }
    else if (state.Ores / b.ClayRobotCost > 0)
    {
        builds.Add((0, 1, 0, 0));
        // If only one of thees can be built add option to wait
        if (state.Ores / b.ClayRobotCost == 1)
            builds.Add((0, 0, 0, 0));

        if (state.Ores / b.OreRobotCost > 0)
            builds.Add((1, 0, 0, 0));
    }
    else if (state.Ores / b.OreRobotCost > 0)
    {
        builds.Add((1, 0, 0, 0));
        // If only one of thees can be built add option to wait
        if (state.Ores / b.OreRobotCost == 1)
            builds.Add((0, 0, 0, 0));
    }
    else
        builds.Add((0, 0, 0, 0));

    // collect resources
    state.Ores += state.OreRobots;
    state.Clays += state.ClayRobots;
    state.Obsidians += state.ObsidianRobots;
    state.GeodeCracked += state.GeodeRobots;

    var maxOpenQeodes = state.GeodeCracked;

    foreach (var buld in builds)
    {
        var newState = state.Copy();
        newState.Ores -= buld.oreRobots * b.OreRobotCost;
        newState.Ores -= buld.clayRobots * b.ClayRobotCost;

        newState.Ores -= buld.obsidianRobots * b.ObsidianRobotCost.ore;
        newState.Clays -= buld.obsidianRobots * b.ObsidianRobotCost.clay;

        newState.Ores -= buld.geodeRobots * b.GeodeRobotCost.ore;
        newState.Obsidians -= buld.geodeRobots * b.GeodeRobotCost.obsidian;

        if (newState.Ores < 0 || newState.Clays < 0 || newState.Obsidians < 0 || newState.GeodeCracked < 0)
            throw new Exception();

        // Increase robot count
        newState.OreRobots += buld.oreRobots;
        newState.ClayRobots += buld.clayRobots;
        newState.ObsidianRobots += buld.obsidianRobots;
        newState.GeodeRobots += buld.geodeRobots;

        var newMax = Move(b, newState, time + 1, memory);
        maxOpenQeodes = Math.Max(maxOpenQeodes, newMax);
    }

    memory.Add(memItem, maxOpenQeodes);

    return maxOpenQeodes;
}

Console.WriteLine(bs[0].QualityLevel * bs[1].QualityLevel * bs[2].QualityLevel);