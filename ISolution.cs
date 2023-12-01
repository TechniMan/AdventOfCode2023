namespace AdventOfCode2023;

public abstract class ISolution
{
    /// <summary>
    /// Get the input for this puzzle.
    /// </summary>
    /// <param name="number">The name of the input file.</param>
    /// <returns></returns>
    public static string[] GetInput(string number)
    {
        return File.ReadAllLines($"inputs/{number}.txt");
    }
}
