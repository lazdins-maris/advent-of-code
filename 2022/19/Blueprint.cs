namespace AdventOfCode2022
{
    internal class Blueprint
    {
        public Blueprint(int id, int oreRobotCost, int clayRobotCost, (int ore, int clay) obsidianRobotCost, (int ore, int obsidian) geodeRobotCost)
        {
            Id = id;
            OreRobotCost = oreRobotCost;
            ClayRobotCost = clayRobotCost;
            ObsidianRobotCost = obsidianRobotCost;
            GeodeRobotCost = geodeRobotCost;
        }

        public int Id { get; }
        public int OreRobotCost { get; }
        public int ClayRobotCost { get; }
        public (int ore, int clay) ObsidianRobotCost { get; }
        public (int ore, int obsidian) GeodeRobotCost { get; }

        public int QualityLevel { get; private set; }

        public void SetQualityLevel(int level)
        {
            QualityLevel = level;
        }

        public override string ToString()
        {
            return $"Blueprint {Id}: Each ore robot costs {OreRobotCost} ore. Each clay robot costs {ClayRobotCost} ore. Each obsidian robot costs {ObsidianRobotCost.ore} ore and {ObsidianRobotCost.clay} clay. Each geode robot costs {GeodeRobotCost.ore} ore and {GeodeRobotCost.obsidian} obsidian.";
        }
    }
}
