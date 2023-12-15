namespace AdventOfCode2023;

class Day15 : ISolution
{
    private static int Hash(string str)
    {
        var hash = 0;
        foreach (var c in str)
        {
            // get ascii code for c
            var ascii = c & 0xFF;
            // add to current value
            hash += ascii;
            // multiply current value by 17
            hash *= 17;
            // remainder of dividing by 256
            hash %= 256;
        }
        return hash;
    }

    public static void Solve()
    {
        var input = GetInput("15")[0].Split(",");

        var answer1 = 0;
        foreach (var instruction in input)
        {
            answer1 += Hash(instruction);
        }

        Console.WriteLine($"Part 1: {answer1}");
    }
}
