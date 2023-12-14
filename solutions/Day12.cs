using System.Text.RegularExpressions;

namespace AdventOfCode2023;

class Day12 : ISolution
{
    public static void Solve()
    {
        var input = GetInput("12");

        // foreach line
        var answer1 = 0;
        // foreach (var line in input)
        var line = input[0];
        {
            var split = line.Split(' ');
            var pattern = split[0];
            var contiguousGroups = split[1].Split(',');
            var matches = Regex.Matches(pattern, $"([#?]{{{contiguousGroups[0]}}})");
            Console.WriteLine(string.Join(", ", matches.Select(m => m.Value)));
            answer1 += matches.Count;
        }

        Console.WriteLine($"Part 1: {answer1}");
    }
}
