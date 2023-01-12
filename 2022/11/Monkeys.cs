namespace AdventOfCode2022
{
    internal abstract class Monkey
    {
        public Monkey(int id, List<Int64> items)
        {
            Id = id;
            Items = items;
        }

        public int Id { get; }
        public List<Int64> Items { get; }
        public long InspectTimes { get; private set; }

        public abstract Int64 Operate(Int64 oldItem);

        public virtual int TestAndThrow(Int64 item)
        {
            InspectTimes++;
            return 0;
        }

        public void ReceiveItem(Int64 item)
        {
            Items.Add(item);
        }
    }

    internal class Monkey0 : Monkey
    {
        public Monkey0() : base(0, new List<Int64>() { 59, 65, 86, 56, 74, 57, 56 })
        {
        }

        public override Int64 Operate(Int64 oldItem)
        {
            return oldItem * 17;
        }

        public override int TestAndThrow(Int64 item)
        {
            base.TestAndThrow(item);
            return item % 3 == 0 ? 3 : 6;
        }
    }
    internal class Monkey1 : Monkey
    {
        public Monkey1() : base(1, new List<Int64>() { 63, 83, 50, 63, 56 })
        {
        }

        public override Int64 Operate(Int64 oldItem)
        {
            return oldItem + 2;
        }

        public override int TestAndThrow(Int64 item)
        {
            base.TestAndThrow(item);
            return item % 13 == 0 ? 3 : 0;
        }
    }
    internal class Monkey2 : Monkey
    {
        public Monkey2() : base(2, new List<Int64>() { 93, 79, 74, 55 })
        {
        }

        public override Int64 Operate(Int64 oldItem)
        {
            return oldItem + 1;
        }

        public override int TestAndThrow(Int64 item)
        {
            base.TestAndThrow(item);
            return item % 2 == 0 ? 0 : 1;
        }
    }
    internal class Monkey3 : Monkey
    {
        public Monkey3() : base(3, new List<Int64>() { 86, 61, 67, 88, 94, 69, 56, 91 })
        {
        }

        public override Int64 Operate(Int64 oldItem)
        {
            return oldItem + 7;
        }

        public override int TestAndThrow(Int64 item)
        {
            base.TestAndThrow(item);
            return item % 11 == 0 ? 6 : 7;
        }
    }
    internal class Monkey4 : Monkey
    {
        public Monkey4() : base(4, new List<Int64>() { 76, 50, 51 })
        {
        }

        public override Int64 Operate(Int64 oldItem)
        {
            return oldItem * oldItem;
        }

        public override int TestAndThrow(Int64 item)
        {
            base.TestAndThrow(item);
            return item % 19 == 0 ? 2 : 5;
        }
    }
    internal class Monkey5 : Monkey
    {
        public Monkey5() : base(5, new List<Int64>() { 77, 76 })
        {
        }

        public override Int64 Operate(Int64 oldItem)
        {
            return oldItem + 8;
        }

        public override int TestAndThrow(Int64 item)
        {
            base.TestAndThrow(item);
            return item % 17 == 0 ? 2 : 1;
        }
    }
    internal class Monkey6 : Monkey
    {
        public Monkey6() : base(6, new List<Int64>() { 74 })
        {
        }

        public override Int64 Operate(Int64 oldItem)
        {
            return oldItem * 2;
        }

        public override int TestAndThrow(Int64 item)
        {
            base.TestAndThrow(item);
            return item % 5 == 0 ? 4 : 7;
        }
    }
    internal class Monkey7 : Monkey
    {
        public Monkey7() : base(7, new List<Int64>() { 86, 85, 52, 86, 91, 95 })
        {
        }

        public override Int64 Operate(Int64 oldItem)
        {
            return oldItem + 6;
        }

        public override int TestAndThrow(Int64 item)
        {
            base.TestAndThrow(item);
            return item % 7 == 0 ? 4 : 5;
        }
    }
}
