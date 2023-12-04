namespace AdventOfCode2023;

class Day04 : ISolution {
  public static void Solve() {
    var input = GetInput("04");

    var answer1 = 0;
    var copies = new List<int>();
    // initialise copies - we start with 1 of each
    for (var c = 0; c < input.Count; ++c) {
      copies.Add(1);
    }

    // foreach line/ticket/game
    for (var game = 0; game < input.Count; ++game) {
      var line = input[game];
      // cut off "Game X:"
      line = line.Split(": ")[1];
      // split the winning numbers from those on the ticket
      var splitLine = line.Split(" | ");
      var winningNumbers = splitLine[0].Split(' ').Where(n => n.Length > 0);
      var ticketNumbers = splitLine[1].Split(' ').Where(n => n.Length > 0);
      // determine any matches
      var numMatches = ticketNumbers.Count(winningNumbers.Contains);

      // PART 1 - determine points
      var points = numMatches > 0 ? (numMatches > 1 ? (2 << (numMatches - 2)) : 1) : 0;
      answer1 += points;

      Console.WriteLine($"G:{game + 1} - C:{copies[game]} - M:{numMatches} - P:{points} - A:{answer1}");

      // PART 2 - ADD COPIES
      for (var futureGame = game + 1; futureGame < game + numMatches + 1; ++futureGame) {
        copies[futureGame] += copies[game];
      }
    }

    Console.WriteLine($"Part 1: {answer1}");
    var answer2 = copies.Sum();
    Console.WriteLine($"Part 2: {answer2}");
  }
}