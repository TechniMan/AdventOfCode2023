namespace AdventOfCode2023;

class Day01 : ISolution
{
    /// <summary>
    /// Read all lines of input.
    /// Find the calibration value of each one.
    /// Sum together.
    /// Output.
    /// </summary>
    public static void Solve()
    {
        // 1 - read input
        var input = GetInput("01");

        // 2 - find answer
        var calibrationValues = new List<int>();
        foreach (var line in input)
        {
            int value = 0;
            int digit = 0;
            foreach (var c in line)
            {
                if (int.TryParse(c.ToString(), out int n))
                {
                    digit = n;
                    // if first digit found, add as tens
                    if (value == 0)
                    {
                        value += n * 10;
                    }
                }
            }
            // at the end, add the final digit we found
            // this could include the only digit of the line, e.g. if the only digit on the line is '7' then the answer is '77'
            value += digit;
            calibrationValues.Add(value);
        }

        // 3 - output solution
        Console.WriteLine($"Part 1 answer: {calibrationValues.Sum()}");
    }
}
