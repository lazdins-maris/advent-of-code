Console.WriteLine("This time I'm runing 1 milion iterations and storing data in the files");
Console.WriteLine("Used those data from file and Excel to find a pattern and calculate rest.");

var lines = File.ReadAllLines("Input.txt");
var jets = lines[0];
var jetIndex = 0;

var shapes = new (int width, int height, bool[,] shape)[] {
    (4, 1, new [,] {
        { true, true, true, true }
    }),
    (3, 3, new [,] {
        { false, true, false },
        { true, true, true },
        { false, true, false },
    }),
    (3, 3, new [,] {
        { false, false, true },
        { false, false, true },
        { true, true, true },
    }),
    (1, 4, new [,] {
        { true },
        { true },
        { true },
        { true }
    }),
    (2, 2, new [,] {
        { true, true },
        { true, true }
    })
};

var shapeIndex = 0;

var highestPoint = -1; // Floor
var chamber = new List<bool[]>();

var points = new List<int>() { -1 };
var deltas = new List<int>();

for (int i = 0; i < 1_000_000; i++) // 1_000_000_000_000
{
    if (i % 5 == 0)
    {
        deltas.Add(highestPoint - points.Last());
        points.Add(highestPoint);
    }

    GrowChamber(chamber, highestPoint);

    var shape = shapes[shapeIndex];
    var shapeTopLeft = ((int left, int top))(2, highestPoint + 3 + shape.height);
    bool canFall;

    do
    {
        // Jet streem
        var jet = jets[jetIndex];
        jetIndex++;
        if (jetIndex == jets.Length)
            jetIndex = 0;

        if (jet == '<' && shapeTopLeft.left > 0 && NotOverlap(chamber, shape, (shapeTopLeft.left - 1, shapeTopLeft.top)))
        {
            shapeTopLeft = (shapeTopLeft.left - 1, shapeTopLeft.top);
        }
        else if (jet == '>' && shapeTopLeft.left + shape.width < 7 && NotOverlap(chamber, shape, (shapeTopLeft.left + 1, shapeTopLeft.top)))
        {
            shapeTopLeft = (shapeTopLeft.left + 1, shapeTopLeft.top);
        }

        // Fall one unit
        canFall = shapeTopLeft.top - shape.height >= 0 && NotOverlap(chamber, shape, (shapeTopLeft.left, shapeTopLeft.top - 1));
        if (canFall)
        {
            shapeTopLeft = (shapeTopLeft.left, shapeTopLeft.top - 1);
        }
    } while (canFall);

    // Freeze shape
    for (int y = 0; y < shape.height; y++)
    {
        for (int x = 0; x < shape.width; x++)
        {
            if (shape.shape[y, x])
                chamber[shapeTopLeft.top - y][shapeTopLeft.left + x] = true;
        }
    }
    highestPoint = Math.Max(highestPoint, shapeTopLeft.top);

    shapeIndex++;
    if (shapeIndex > 4)
        shapeIndex = 0;
}

bool NotOverlap(List<bool[]> chamber, (int width, int height, bool[,] shape) shape, (int left, int top) shapeTopLeft)
{
    for (int y = 0; y < shape.height; y++)
    {
        for (int x = 0; x < shape.width; x++)
        {
            if (shape.shape[y, x] && chamber[shapeTopLeft.top - y][shapeTopLeft.left + x])
                return false;
        }
    }

    return true;
}

void GrowChamber(List<bool[]> chamber, int highestPoint)
{
    var expectedLength = (highestPoint + 1) + 3 + 4;
    var needExtra = expectedLength - chamber.Count();
    for (int i = 0; i < needExtra; i++)
    {
        chamber.Add(new bool[7] { false, false, false, false, false, false, false });
    }
}

Console.WriteLine(highestPoint + 1);

File.WriteAllLines("points.txt", points.Select(_ => _.ToString()));
File.WriteAllLines("deltas.txt", deltas.Select(_ => _.ToString()));