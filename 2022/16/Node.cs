namespace AdventOfCode2022
{
    internal class Node
    {
        public Node(string name, int flow, string[] childs)
        {
            Name = name;
            Flow = flow;
            Childs = childs;
        }

        public string Name { get; }
        public int Flow { get; }
        public string[] Childs { get; }

        public override string ToString()
        {
            return $"{Name} -> {Flow} ({string.Join(", ", Childs)})";
        }
    }
}
