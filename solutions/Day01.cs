using System.Text.RegularExpressions;

namespace AdventOfCode2023;

class Day01 : ISolution
{
    private static List<string> Indices = new() {
        "zero", // unused; but buffers up the indices :)
        "one",
        "two",
        "three",
        "four",
        "five",
        "six",
        "seven",
        "eight",
        "nine"
    };

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
        var answer1 = 0;
        var answer2 = 0;
        var n = 0;
        foreach (var line in input)
        {
            // PART 1
            int value = 0;
            int digit = 0;
            foreach (var c in line)
            {
                if (int.TryParse(c.ToString(), out n))
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
            answer1 += value;

            // PART 2
            value = 0;
            var match = Regex.Match(line, "(\\d|one|two|three|four|five|six|seven|eight|nine)").Value;
            var v = int.TryParse(match, out n) ? n : Indices.IndexOf(match);
            value += v * 10;
            match = Regex.Match(line, "(\\d|one|two|three|four|five|six|seven|eight|nine)", RegexOptions.RightToLeft).Value;
            v = int.TryParse(match, out n) ? n : Indices.IndexOf(match);
            value += v;
            answer2 += value;
        }

        // 3 - output solution
        Console.WriteLine($"Part 1 answer: {answer1}");
        Console.WriteLine($"Part 2 answer: {answer2}");
    }
}
