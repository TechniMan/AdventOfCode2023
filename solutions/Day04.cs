namespace AdventOfCode2023;

class Day04 : ISolution {
  public static void Solve() {
    var input = GetInput("04");

    var answer1 = 0;
    // foreach line/ticket/game
    for (var gameId = 1; gameId < input.Count; ++gameId) {
      var line = input[gameId - 1];
      // cut off "Game X:"
      line = line.Split(": ")[1];
      // split the winning numbers from those on the ticket
      var splitLine = line.Split(" | ");
      var winningNumbers = splitLine[0].Split(' ').Where(n => n.Length > 0);
      var ticketNumbers = splitLine[1].Split(' ').Where(n => n.Length > 0);
      // determine any matches
      var numMatches = ticketNumbers.Count(winningNumbers.Contains);

      // determine points
      var points = numMatches > 0 ? (numMatches > 1 ? (2 << (numMatches - 2)) : 1) : 0;
      answer1 += points;

      // Console.WriteLine($"G:{gameId} - M:{numMatches} - P:{points} - A:{answer1}");
    }

    Console.WriteLine($"Part 1: {answer1}");
  }
}