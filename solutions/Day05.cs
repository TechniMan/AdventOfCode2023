namespace AdventOfCode2023;

class Day05 : ISolution {
  public static void Solve() {
    // process input
    var input = GetInput("05");
    var initialSeeds = input[0].Split(": ")[1].Split(" ").Select(s => int.Parse(s)).ToList();

    // map reading:
    // destinationStart sourceStart rangeLength

    // find answer
    var answer1 = 0;
    Console.WriteLine($"Part 1: {answer1}");
  }
}