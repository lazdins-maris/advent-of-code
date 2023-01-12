namespace AdventOfCode2022
{
    public class Sorter
    {
        public static List<Num> Sort1(List<Num> ns)
        {
            var target = new List<Num>(ns);

            foreach (var num in ns)
            {
                var n = num.N % (ns.Count - 1);

                // 0 1 3 3 4 5 (count == 6)
                //     i

                if (n == 0)
                {
                    continue;
                }

                long newPosition;
                int ii = target.IndexOf(num);

                if (n > 0)
                {
                    newPosition = (ii + n) % ns.Count + 1;
                }
                else
                {
                    newPosition = ((ii + ns.Count + (n % ns.Count)) % ns.Count);
                }

                if (newPosition > ii)
                {
                    target.Insert((int)newPosition, num);
                    target.RemoveAt(ii);
                }
                else
                {
                    target.RemoveAt(ii);
                    target.Insert((int)newPosition, num);
                }
            }

            return target;
        }
        public static List<Num> Sort2(List<Num> ns, List<Num> target)
        {
            foreach (var num in ns)
            {
                var n = num.N % (ns.Count - 1);

                // 0 1 3 3 4 5 (count == 6)
                //     i

                if (n == 0)
                {
                    continue;
                }

                long newPosition;
                int ii = target.IndexOf(num);

                if (n > 0)
                {
                    newPosition = (ii + n) % ns.Count + 1;
                }
                else
                {
                    newPosition = ((ii + ns.Count + (n % ns.Count)) % ns.Count);
                }

                if (newPosition > ii)
                {
                    target.Insert((int)newPosition, num);
                    target.RemoveAt(ii);
                }
                else
                {
                    target.RemoveAt(ii);
                    target.Insert((int)newPosition, num);
                }
            }

            return target;
        }
    }
}