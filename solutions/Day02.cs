namespace AdventOfCode2023;

class Day02 : ISolution {
    public static void Solve() {
        /// 1 - READ INPUT
        var inputLines = GetInput("02");
        var part1Limits = new Dictionary<string, int> {
            { "red", 12 },
            { "green", 13 },
            { "blue", 14 }
        };

        /// 2 - CALCULATE ANSWER
        var answer1 = 0;
        // foreach game
        for (int gameId = 1; gameId < 101; gameId++) {
            // get sets of cubes
            var cubeSets = inputLines[gameId - 1].Split(':')[1].Split(';');

            // find greatest quantity of each colour this game
            var highestGameCubes = new Dictionary<string, int> {
                { "red", 0 },
                { "green", 0 },
                { "blue", 0 }
            };

            // foreach set of cubes
            foreach (var set in cubeSets) {
                var colours = set.Split(',');

                // foreach colour
                foreach (var colour in colours) {
                    // get each colour and quantity
                    var split = colour.Split(' ');
                    var q = int.Parse(split[1]);
                    var c = split[2];

                    if (highestGameCubes[c] < q) {
                        highestGameCubes[c] = q;
                    }
                }
            }

            // check game is possible for criteria
            var possible = true;
            // if any quantity of colour is greater than the limits, then it's an impossible game
            foreach (var colour in part1Limits.Keys) {
                if (highestGameCubes[colour] > part1Limits[colour]) {
                    possible = false;
                    break;
                }
            }
            // if possible, add the game ID to get the answer
            if (possible) {
                answer1 += gameId;
            }
        }

        /// 3 - OUTPUT ANSWER
        Console.WriteLine($"Part 1: {answer1}");
    }
}
