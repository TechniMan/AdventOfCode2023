﻿namespace AdventOfCode2023 {
    internal class Program {
        static Dictionary<string, Action> Days = new() {
            { "1", Day01.Solve },
            { "2", Day02.Solve },
            { "3", Day03.Solve },
            { "4", Day04.Solve },
        };

        static void Main(string[] args) {
            if (args.Length != 1) {
                Console.WriteLine($"Invalid args: {string.Join(", ", args)}.");
                return;
            }

            if (!Days.ContainsKey(args[0])) {
                Console.WriteLine($"Unrecognised puzzle ID: {args[0]}.");
            }

            Days[args[0]]();
        }
    }
}
