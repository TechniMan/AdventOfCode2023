namespace AdventOfCode2023;

class Day09 : ISolution
{
    public static void Solve()
    {
        var maze = GetInput("09");

        // interpret the maze

        // find S tile
        var x = 0;
        var y = 0;
        for (y = 0; y < maze.Count; ++y)
        {
            for (x = 0; x < maze[0].Length; ++x)
            {
                if (maze[y][x] == 'S')
                {
                    goto found;
                }
            }
        }
    // found S - leave the loop
    found:
        Console.WriteLine($"Found start: {x}, {y}");

        // PART 1 start crawling through to find the furthest distance pipe
        var completedLoop = false;
        var totalSteps = 0;
        var prevX = x;
        var prevY = y;
        x--; // start off in a direction manually lol
        while (!completedLoop)
        {
            totalSteps++;
            var symbol = maze[y][x];

            switch (symbol)
            {
                case '|':
                    if (prevY < y)
                    {
                        prevX = x;
                        prevY = y;
                        y++;
                    }
                    else
                    {
                        prevX = x;
                        prevY = y;
                        y--;
                    }
                    break;
                case '-':
                    if (prevX < x)
                    {
                        prevX = x;
                        prevY = y;
                        x++;
                    }
                    else
                    {
                        prevX = x;
                        prevY = y;
                        x--;
                    }
                    break;
                case 'L':
                    if (prevY < y)
                    {
                        prevX = x;
                        prevY = y;
                        x++;
                    }
                    else
                    {
                        prevX = x;
                        prevY = y;
                        y--;
                    }
                    break;
                case 'J':
                    if (prevY < y)
                    {
                        prevX = x;
                        prevY = y;
                        x--;
                    }
                    else
                    {
                        prevX = x;
                        prevY = y;
                        y--;
                    }
                    break;
                case '7':
                    if (prevY > y)
                    {
                        prevX = x;
                        prevY = y;
                        x--;
                    }
                    else
                    {
                        prevX = x;
                        prevY = y;
                        y++;
                    }
                    break;
                case 'F':
                    if (prevY > y)
                    {
                        prevX = x;
                        prevY = y;
                        x++;
                    }
                    else
                    {
                        prevX = x;
                        prevY = y;
                        y++;
                    }
                    break;
                // reached the start again
                case 'S':
                    completedLoop = true;
                    break;
                // ground or unknown - shouldn't happen
                case '.':
                default:
                    throw new Exception("wat");
            }

            // Console.Write($"Found a {symbol} pipe\r");
        }

        var answer1 = totalSteps / 2;
        Console.WriteLine($"Part 1: {answer1}");
    }
}
