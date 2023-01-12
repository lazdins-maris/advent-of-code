using System.Numerics;

namespace AdventOfCode2022
{
    public static class Snafu
    {
        private static BigInteger[] coef = new BigInteger[] {
            1,
            5,
            5 * 5,
            5 * 5 * 5,
            5 * 5 * 5 * 5,
            5 * 5 * 5 * 5 * 5,
            5 * 5 * 5 * 5 * 5 * 5,
            5 * 5 * 5 * 5 * 5 * 5 * 5,
            5 * 5 * 5 * 5 * 5 * 5 * 5 * 5,
            5 * 5 * 5 * 5 * 5 * 5 * 5 * 5 * 5,
            5 * 5 * 5 * 5 * 5 * 5 * 5 * 5 * 5 * 5,
            5 * 5 * 5 * 5 * 5 * 5 * 5 * 5 * 5 * 5 * 5,
            5 * 5 * 5 * 5 * 5 * 5 * 5 * 5 * 5 * 5 * 5 * 5,
            5 * 5 * 5 * 5 * 5 * 5 * 5 * 5 * 5 * 5 * 5 * 5 * 5,
            new BigInteger(5 * 5 * 5 * 5 * 5 * 5 * 5 * 5 * 5 * 5 * 5 * 5 * 5) * 5,
            new BigInteger(5 * 5 * 5 * 5 * 5 * 5 * 5 * 5 * 5 * 5 * 5 * 5 * 5) * 5 * 5,
            new BigInteger(5 * 5 * 5 * 5 * 5 * 5 * 5 * 5 * 5 * 5 * 5 * 5 * 5) * 5 * 5 * 5,
            new BigInteger(5 * 5 * 5 * 5 * 5 * 5 * 5 * 5 * 5 * 5 * 5 * 5 * 5) * 5 * 5 * 5 * 5,
            new BigInteger(5 * 5 * 5 * 5 * 5 * 5 * 5 * 5 * 5 * 5 * 5 * 5 * 5) * 5 * 5 * 5 * 5 * 5,
            new BigInteger(5 * 5 * 5 * 5 * 5 * 5 * 5 * 5 * 5 * 5 * 5 * 5 * 5) * 5 * 5 * 5 * 5 * 5 * 5,
        };

        private static Dictionary<char, long> map = new Dictionary<char, long>()
        {
            { '2', 2 },
            { '1', 1 },
            { '0', 0 },
            { '-', -1 },
            { '=', -2 }
        };

        public static BigInteger ToNumber(string snafu)
        {
            BigInteger rez = 0;
            var sn = snafu.Reverse().ToArray();

            for (int i = 0; i < sn.Length; i++)
            {
                rez += map[sn[i]] * coef[i];
            }

            return rez;
        }

        private static Dictionary<long, string> mapR = new Dictionary<long, string>()
        {
            { 2, "2" },
            { 1, "1" },
            { 0, "0" },
            { 4, "-" },
            { 3, "=" }
        };

        public static string ToSnafu(long number)
        {
            var rez = "";

            do
            {
                var p1 = number % 5;
                var p2 = number / 5;

                rez = mapR[p1] + rez;
                if (p1 == 3 || p1 == 4)
                    p2++;

                number = p2;

            } while (number > 0);

            return rez;
        }

    }
}
