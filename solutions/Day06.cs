namespace AdventOfCode2023;

class Day06: ISolution {
    private static int Distance(int timeHeldButton, int timeRemaining) => timeHeldButton * timeRemaining;

    public static void Solve() {
        var input = GetInput("06");

        // parse input
        var times = input[0].Split(": ")[1].Split(' ').Select(int.Parse).ToList();
        var distances = input[1].Split(": ")[1].Split(' ').Select(int.Parse).ToList();

        var answer1 = 1;
        for (var race = 0; race < times.Count; ++race) {
            var numberOfWaysToWin = 0;

            // find the fewest milliseconds required to win
            for (var timeHeldButton = 0; timeHeldButton < times[race]; ++timeHeldButton) {
                Console.Write($"Holding button for {timeHeldButton}ms / {times[race]}ms\r");

                if (Distance(timeHeldButton, times[race] - timeHeldButton) > distances[race]) {
                    numberOfWaysToWin++;
                }
            }

            // mulitply to get the answer
            answer1 *= numberOfWaysToWin;
            Console.WriteLine($"Finished race {race} / {times.Count}");
        }

        Console.WriteLine($"Part 1: {answer1}");
    }
}
