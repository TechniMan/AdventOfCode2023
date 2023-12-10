namespace AdventOfCode2023;

class Day09 : ISolution
{
  public static void Solve()
  {
    var input = GetInput("08");

    var answer1 = 0L;
    var answer2 = 0L;
    foreach (var line in input)
    {
      var sequence = line.Split(' ').Select(int.Parse).ToList();
      var differences = new List<List<int>> {
        new(sequence)
      };

      // until we find a diffs list of all 0s
      while (!differences.Last().All(i => i == 0))
      {
        // find the diffs of the previous sequence
        sequence.Clear();
        for (var i = 1; i < differences.Last().Count; ++i)
        {
          sequence.Add(differences.Last()[i] - differences.Last()[i - 1]);
        }
        differences.Add(new List<int>(sequence));
      }

      // PART 1
      // extend the last sequence with a 0
      differences.Last().Add(0);
      // working backwards, determine next in each sequence
      for (var s = differences.Count - 2; s >= 0; --s)
      {
        // find the increment from the next sequence down
        var inc = differences[s + 1].Last();
        // extend the sequence by one
        differences[s].Add(differences[s].Last() + inc);
      }
      answer1 += differences[0].Last();

      // PART 2
      // working backwards, determine next BEFORE each sequence
      for (var s = differences.Count - 2; s >= 0; --s)
      {
        // find the increment from the next sequence down
        var inc = differences[s + 1].First();
        // extend the sequence by one
        differences[s].Insert(0, differences[s].First() - inc);
      }
      Console.WriteLine(string.Join(", ", differences[0]));
      answer2 += differences[0].First();
    }

    Console.WriteLine($"Part 1: {answer1}");
    Console.WriteLine($"Part 2: {answer2}");
  }
}
