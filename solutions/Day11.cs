using System.Drawing;

namespace AdventOfCode2023;

class Galaxy(int id, int x, int y)
{
    public int Id = id;
    public Point Position = new(x, y);

    public int DistanceTo(Galaxy other)
    {
        // add absdiff in x to absdiff in y
        return Math.Abs(Position.X - other.Position.X) + Math.Abs(Position.Y - other.Position.Y);
    }

    public static int operator -(Galaxy self, Galaxy other)
    {
        return self.DistanceTo(other);
    }
}

class Day11 : ISolution
{
    const int INPUT_LENGTH = 140;
    public static void Solve()
    {
        var input = GetInput("11");

        // catalogue all galaxies
        var catalogue = new List<Galaxy>();
        // determine rows which expand
        var expandedRows = Enumerable.Repeat(true, INPUT_LENGTH).ToArray();
        // determine columns which expand
        var expandedCols = Enumerable.Repeat(true, INPUT_LENGTH).ToArray();

        for (var y = 0; y < INPUT_LENGTH; ++y)
        {
            for (var x = 0; x < INPUT_LENGTH; ++x)
            {
                if (input[y][x] == '#')
                {
                    // catalogue the galaxy ...
                    catalogue.Add(new Galaxy(catalogue.Count, x, y));
                    // ... and discount row and column from expansion
                    expandedCols[x] = false;
                    expandedRows[y] = false;
                }
            }
        }

        var pairCount = Enumerable.Range(0, INPUT_LENGTH).Sum();
        Console.WriteLine($"Found {catalogue.Count} galaxies; {pairCount} pairs.");

        //TODO do the math
        var answer1 = 0;
        for (var g = 0; g < catalogue.Count - 1; ++g)
        {
            for (var h = g + 1; h < catalogue.Count; ++h)
            {
                var galaxy1 = catalogue[g];
                var galaxy2 = catalogue[h];
                // find the distance for this pair
                answer1 += galaxy2 - galaxy1;

                // ill-advised method to add expansion on
                var lowY = 0;
                var highY = 0;
                if (galaxy1.Position.Y < galaxy2.Position.Y)
                {
                    lowY = galaxy1.Position.Y;
                    highY = galaxy2.Position.Y;
                }
                else
                {
                    lowY = galaxy2.Position.Y;
                    highY = galaxy1.Position.Y;
                }
                var lowX = 0;
                var highX = 0;
                if (galaxy1.Position.X < galaxy2.Position.X)
                {
                    lowX = galaxy1.Position.X;
                    highX = galaxy2.Position.X;
                }
                else
                {
                    lowX = galaxy2.Position.X;
                    highX = galaxy1.Position.X;
                }

                for (var y = lowY; y < highY; ++y)
                {
                    if (expandedRows[y])
                    {
                        answer1++;
                    }
                }
                for (var x = lowX; x < highX; ++x)
                {
                    if (expandedCols[x])
                    {
                        answer1++;
                    }
                }
            }
        }

        Console.WriteLine($"Part 1: {answer1}");
    }
}
