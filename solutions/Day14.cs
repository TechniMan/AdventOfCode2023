using System.Drawing;

namespace AdventOfCode2023;

class Day14 : ISolution
{
    private const int MAP_WIDTH = 100;

    public static void Solve()
    {
        var input = GetInput("14");

        var map = new bool[MAP_WIDTH, MAP_WIDTH];
        var rocks = new List<Point>();
        for (var y = 0; y < MAP_WIDTH; ++y)
        {
            for (var x = 0; x < MAP_WIDTH; ++x)
            {
                // set blocking rocks to TRUE
                map[y, x] = input[y][x] == '#';
                if (input[y][x] == 'O')
                {
                    map[y, x] = true;
                    rocks.Add(new Point(x, y));
                }
            }
        }

        // try sorting the rocks
        rocks = [.. rocks.OrderBy(r => r.X + (100 * r.Y))];

        // 'tilt' the map north and move the rocks appropriately
        var answer1 = 0;
        for (var r = 0; r < rocks.Count; ++r)
        {
            var rock = rocks[r];
            // the way the rocks are added into the list, they are
            //  already in order of top to bottom
            //  therefore safe to move up
            // scan for the most northerly space the rock can move into

            // start the y at the space above the rock unless we're already at the top
            //  we don't want the rock itself to block our movement
            var y = rock.Y;
            while (true)
            {
                y--;
                if (y <= 0) { y = 0; break; }
                if (map[y, rock.X])
                {
                    // we've found a space blocking the rock
                    // so move it to the space before
                    y++;
                    break;
                }
            }
            // move it, inc. updating collision map
            map[rock.Y, rock.X] = false;
            try
            {
                map[y, rock.X] = true;
            }
            catch (Exception)
            {
                Console.WriteLine($"{y}, {rock.X}");
            }
            rocks[r] = new Point(rock.X, y);
            // add 'load' to answer1
            var load = MAP_WIDTH - y;
            answer1 += load;
        }

        // print out the new map
        var cumulativeLoad = 0;
        Console.WriteLine($"{' ',100} | lod * rks = line | cumulativeLoad");
        for (var y = 0; y < input.Count; ++y)
        {
            // create the line of the map after the tilt
            var line = "".PadLeft(MAP_WIDTH, '.');

            // fill in the square rocks
            for (var x = 0; x < MAP_WIDTH; ++x)
            {
                if (map[y, x])
                {
                    var start = x > 0 ? x : 0;
                    var end = x < line.Length ? x + 1 : line.Length;
                    line = $"{line[..start]}#{line[end..]}";
                }
            }

            // fill in the round rocks
            var lineRocks = rocks.Where(r => r.Y == y);
            foreach (var r in lineRocks)
            {
                var start = r.X > 0 ? r.X : 0;
                var end = r.X < line.Length ? r.X + 1 : line.Length;
                line = $"{line[..start]}O{line[end..]}";
            }

            // print the map
            var lineLoad = lineRocks.Count() * (MAP_WIDTH - y);
            cumulativeLoad += lineLoad;
            Console.WriteLine($"{line} | {MAP_WIDTH - y,3} * {lineRocks.Count(),3} = {lineLoad,4} | {cumulativeLoad,6}");
        }

        // 106328 too high
        // 106267 too high
        Console.WriteLine($"Part 1: {answer1}");
    }
}
