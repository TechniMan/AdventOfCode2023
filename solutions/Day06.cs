namespace AdventOfCode2023;

class Day06: ISolution {
    private static int Distance(int timeHeldButton, int timeRemaining) => timeHeldButton * timeRemaining;
    private static long Distance(long timeHeldButton, long timeRemaining) => timeHeldButton * timeRemaining;

    public static void Solve() {
        var input = GetInput("06");

        // parse input
        var times = input[0].Split(": ")[1].Split(' ').Select(int.Parse).ToList();
        var distances = input[1].Split(": ")[1].Split(' ').Select(int.Parse).ToList();
        var time2 = long.Parse(string.Join("", times));
        var distance2 = long.Parse(string.Join("", distances));

        // PART 1
        var answer1 = 1;
        var waysToWin = 0;
        for (var race = 0; race < times.Count; ++race) {
            waysToWin = 0;

            // find the fewest milliseconds required to win
            for (var timeHeldButton = 0; timeHeldButton < times[race]; ++timeHeldButton) {
                Console.Write($"Holding button for {timeHeldButton}ms / {times[race]}ms\r");

                if (Distance(timeHeldButton, times[race] - timeHeldButton) > distances[race]) {
                    waysToWin++;
                }
            }

            // mulitply to get the answer
            answer1 *= waysToWin;
            Console.WriteLine($"Finished race {race} / {times.Count}");
        }
        Console.WriteLine($"Part 1: {answer1}");

        // PART 2
        var answer2 = 0;
        // find the fewest milliseconds required to win
        for (var timeHeldButton = 0; timeHeldButton < time2; ++timeHeldButton) {
            if (timeHeldButton % 10000 == 0) {
                Console.Write($"Holding button for {timeHeldButton}ms / {time2}ms\r");
            }

            if (Distance(timeHeldButton, time2 - timeHeldButton) > distance2) {
                answer2++;
            }
        }
        // multiply to get the answer
        Console.WriteLine();
        Console.WriteLine($"Part 2: {answer2}");
    }
}
