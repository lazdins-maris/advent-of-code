namespace AdventOfCode2022
{
    public class Num
    {
        public Num(long n)
        {
            N = n;
        }

        public long N { get; }

        public override string ToString()
        {
            return N.ToString();
        }
    }
}
