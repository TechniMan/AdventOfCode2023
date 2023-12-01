namespace AdventOfCode2023
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine($"Incorrect args: {string.Join(", ", args)}.");
                return;
            }

            switch (args[0])
            {
                case "1":
                    Day01.Solve();
                    break;

                default:
                    Console.WriteLine($"Unrecognised puzzle ID: {args[0]}.");
                    return;
            }
        }
    }
}
