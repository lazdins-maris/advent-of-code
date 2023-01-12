namespace AdventOfCode2022
{
    internal class MyComparer : IComparer<Item>
    {
        public int Compare(Item a, Item b)
        {
            var isR = IsRightOrder(a, b);
            if (!isR.HasValue)
                return 0;
            return isR.Value ? -1 : 1;
        }
        static bool? IsRightOrder(Item item1, Item item2)
        {
            if (item1.Value.HasValue && item2.Value.HasValue)
            {
                if (item1.Value == item2.Value)
                    return null;

                return item1.Value < item2.Value;
            }

            if (!item1.Value.HasValue && !item2.Value.HasValue)
            {
                bool? isRight = null;

                if (item1.Child.Length == 0 && item2.Child.Length > 0)
                    return true;

                for (int i = 0; i < item1.Child.Length; i++)
                {
                    if (item2.Child.Length <= i)
                        return false;

                    isRight = IsRightOrder(item1.Child[i], item2.Child[i]);

                    if (isRight.HasValue)
                        return isRight;
                }

                return isRight;
            }

            if (item1.Value.HasValue)
            {
                item1 = new Item(new[] { item1 });
            }
            if (item2.Value.HasValue)
            {
                item2 = new Item(new[] { item2 });
            }

            return IsRightOrder(item1, item2);
        }
    }
}