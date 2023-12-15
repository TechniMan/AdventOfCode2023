using System.Text.RegularExpressions;

namespace AdventOfCode2023;

partial class Day15 : ISolution
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
        var boxes = new Dictionary<int, Dictionary<string, int>>();
        for (var i = 0; i < 256; ++i)
        {
            boxes.Add(i, []);
        }

        var answer1 = 0;
        foreach (var instruction in input)
        {
            answer1 += Hash(instruction);
            var lensLabel = LabelRegex().Match(instruction).Value;
            var boxId = Hash(lensLabel);
            var operationChar = OperationRegex().Match(instruction).Value[0];

            if (operationChar == '-')
            {
                boxes[boxId].Remove(lensLabel);
            }
            else
            {
                var focalLength = int.Parse(FocalLengthRegex().Match(instruction).Value);
                try
                {
                    boxes[boxId][lensLabel] = focalLength;
                }
                catch (KeyNotFoundException)
                {
                    //ignore
                }
            }
        }

        var answer2 = 0;
        // foreach box
        for (var b = 0; b < boxes.Count; ++b)
        {
            var box = boxes[b];
            // foreach lens in box
            for (var l = 0; l < box.Count; ++l)
            {
                var focalPower = (b + 1) * (l + 1) * box.Values.ElementAt(l);
                answer2 += focalPower;
            }
        }

        Console.WriteLine($"Part 1: {answer1}");
        Console.WriteLine($"Part 2: 265133< {answer2} <");
    }

    [GeneratedRegex("\\w+")]
    private static partial Regex LabelRegex();
    [GeneratedRegex("[-=]")]
    private static partial Regex OperationRegex();
    [GeneratedRegex("\\d+")]
    private static partial Regex FocalLengthRegex();
}
