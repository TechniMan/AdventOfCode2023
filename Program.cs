﻿namespace AdventOfCode2023;

internal class Program
{
    static readonly Dictionary<string, Action> Days = new() {
        { "1", Day01.Solve },
        { "2", Day02.Solve },
        { "3", Day03.Solve },
        { "4", Day04.Solve },
        { "5", Day05.Solve },
        { "6", Day06.Solve },
        { "7", Day07.Solve },
        // { "8", Day08.Solve },
        { "9", Day09.Solve },
        { "10", Day10.Solve },
        { "11", Day11.Solve },
        { "12", Day12.Solve },
        // { "13", Day13.Solve },
        { "14", Day14.Solve },
        { "15", Day15.Solve },
        { "16", Day16.Solve },
    };

    static void Main(string[] args)
    {
        if (args.Length != 1)
        {
            Console.WriteLine($"Invalid args: {string.Join(", ", args)}.");
            return;
        }

        if (!Days.ContainsKey(args[0]))
        {
            Console.WriteLine($"Unrecognised puzzle ID: {args[0]}.");
        }

        Days[args[0]]();
    }
}
