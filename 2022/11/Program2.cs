using AdventOfCode2022;

var mons = new List<Monkey>() {
    new Monkey0(),
    new Monkey1(),
    new Monkey2(),
    new Monkey3(),
    new Monkey4(),
    new Monkey5(),
    new Monkey6(),
    new Monkey7(),
};

for (int r = 0; r < 10_000; r++)
{
    foreach (var monkey in mons)
    {
        for (int i = 0; i < monkey.Items.Count; i++)
        {
            var item = monkey.Items[i];
            item = monkey.Operate(item);
            monkey.Items[i] = item;

            var testRez = monkey.TestAndThrow(item);
            // All those numbers coming from Input.txt lines starting with: divisible by ...
            mons[testRez].ReceiveItem(item % (3 * 13 * 2 * 11 * 19 * 17 * 5 * 7));
        }

        monkey.Items.Clear();
    }
}

var orderedMons = mons.OrderByDescending(_ => _.InspectTimes).ToArray();

Console.WriteLine(orderedMons[0].InspectTimes * orderedMons[1].InspectTimes);