using System.Drawing;

namespace AdventOfCode2023;

class Day16 : ISolution
{
    private enum Direction
    {
        North,
        East,
        South,
        West
    }

    private static readonly Dictionary<Direction, Direction> ForwardSlashMap = new() {
        { Direction.East, Direction.North },
        { Direction.North, Direction.East },
        { Direction.West, Direction.South },
        { Direction.South, Direction.West }
    };
    private static readonly Dictionary<Direction, Direction> BackSlashMap = new() {
        { Direction.East, Direction.South },
        { Direction.South, Direction.East },
        { Direction.West, Direction.North },
        { Direction.North, Direction.West }
    };
    private static readonly Dictionary<Direction, Direction[]> HorizontalSplitter = new() {
        { Direction.East, [Direction.East] },
        { Direction.West, [Direction.West] },
        { Direction.North, [Direction.East, Direction.West] },
        { Direction.South, [Direction.East, Direction.West] }
    };
    private static readonly Dictionary<Direction, Direction[]> VerticalSplitter = new() {
        { Direction.East, [Direction.North, Direction.South] },
        { Direction.West, [Direction.North, Direction.South] },
        { Direction.North, [Direction.North] },
        { Direction.South, [Direction.South] }
    };
    private static readonly Dictionary<Direction, Point> MoveAmount = new() {
        { Direction.East, new Point(1, 0) },
        { Direction.South, new Point(0, 1) },
        { Direction.West, new Point(-1, 0) },
        { Direction.North, new Point(0, -1) }
    };

    private class Beam(int x, int y, Direction d)
    {
        public int X = x;
        public int Y = y;
        public Direction D = d;
        public bool Finished = false;
    }

    private static IEnumerable<Beam> StepBeam(Beam beam)
    {
        // guard against beam escaping
        if (beam.X < 0 || beam.X >= MAP_SIZE || beam.Y < 0 || beam.Y >= MAP_SIZE)
        {
            beam.Finished = true;
            return [beam];
        }

        // init result array
        var result = new List<Beam>();

        // change direction based on this cell
        var ch = map[beam.Y][beam.X];
        // add a new beam if needed
        if (ch == '-' && HorizontalSplitter[beam.D].Length > 1)
        {
            result.Add(new Beam(beam.X, beam.Y, HorizontalSplitter[beam.D][1]));
        }
        else if (ch == '|' && VerticalSplitter[beam.D].Length > 1)
        {
            result.Add(new Beam(beam.X, beam.Y, VerticalSplitter[beam.D][1]));
        }
        // then update direction of beam
        beam.D = ch switch
        {
            '-' => HorizontalSplitter[beam.D][0],
            '|' => VerticalSplitter[beam.D][0],
            '/' => ForwardSlashMap[beam.D],
            '\\' => BackSlashMap[beam.D],
            _ => beam.D
        };

        // move the beam along
        beam.X += MoveAmount[beam.D].X;
        beam.Y += MoveAmount[beam.D].Y;
        // guard against beam escaping
        if (beam.X < 0 || beam.X >= MAP_SIZE || beam.Y < 0 || beam.Y >= MAP_SIZE)
        {
            beam.Finished = true;
            result.Add(beam);
            return result;
        }

        // energise the cell
        try { energisedCells[beam.X + (beam.Y * MAP_SIZE)] = true; }
        catch { Console.WriteLine($"{beam.X}, {beam.Y}"); }
        result.Add(beam);
        return result;
    }

    static List<string> map = [];
    static int MAP_SIZE => 110;
    static readonly bool[] energisedCells = Enumerable.Repeat(false, MAP_SIZE * MAP_SIZE).ToArray();

    public static void Solve()
    {
        map = GetInput("16");

        var beams = new List<Beam>() {
            new(0, 0, Direction.East)
        };

        while (beams.Any(b => !b.Finished))
        {
            for (var b = 0; b < beams.Count; ++b)
            {
                // don't simulate finished beams
                if (beams[b].Finished) continue;
                // step the beam along the simulation
                var newBeams = StepBeam(beams[b]);
                // remove old version of beam
                beams.Remove(beams[b]);
                // add in new version of beam plus any extras generated
                beams.AddRange(newBeams);
            }
        }

        var answer1 = energisedCells.Count(c => c);
        Console.WriteLine($"Part 1: {answer1}/12100");
    }
}
