namespace AdventOfCode2022
{
    internal class Item
    {
        public Item(int value)
        {
            Value = value;
        }

        public Item(Item[] child)
        {
            Child = child;
        }

        public int? Value { get; }
        public Item[]? Child { get; }

        public override string ToString()
        {
            if (Value.HasValue)
                return Value.ToString();

            return $"[{string.Join(',', Child.Select(_ => _.ToString()))}]";
        }
    }
}
