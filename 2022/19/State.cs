namespace AdventOfCode2022
{
    internal class State
    {
        public int Ores { get; set; }
        public int Clays { get; set; }
        public int Obsidians { get; set; }
        public int GeodeCracked { get; set; }

        public int OreRobots { get; set; }
        public int ClayRobots { get; set; }
        public int ObsidianRobots { get; set; }
        public int GeodeRobots { get; set; }

        public State Copy()
        {
            return new State
            {
                Ores = Ores,
                Clays = Clays,
                Obsidians = Obsidians,
                GeodeCracked = GeodeCracked,
                OreRobots = OreRobots,
                ClayRobots = ClayRobots,
                ObsidianRobots = ObsidianRobots,
                GeodeRobots = GeodeRobots,
            };
        }

        public override string ToString()
        {
            return $"Robots: {OreRobots}, {ClayRobots}, {ObsidianRobots}, {GeodeRobots} / Resources: {Ores}, {Clays}, {Obsidians}, {GeodeCracked}";
        }
    }
}
