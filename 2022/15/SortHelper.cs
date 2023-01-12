using System.Collections;

namespace AdventOfCode2022
{
    internal class SortHelper : IComparer
    {
        int IComparer.Compare(object a, object b)
        {
            (int x1, int x2) first = ((int x1, int x2))a;
            (int x1, int x2) second = ((int x1, int x2))b;
            if (first.x1 == second.x1)
            {
                return first.x2 - second.x2;
            }
            return first.x1 - second.x1;
        }
    }
}