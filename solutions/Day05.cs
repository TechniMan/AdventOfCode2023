namespace AdventOfCode2023;

class Mapping
{
    public readonly struct LongRange(long start, long end)
    {
        public readonly long Start = start;
        public readonly long End = end;

        public bool Contains(long value) => value >= Start && value <= End;
        public long IndexOf(long value) => value - Start;
        public long ValueOf(long index) => Start + index;
    }

    /// <summary>
    /// key: SOURCE range
    /// value: DESTINATION range
    /// </summary>
    public readonly Dictionary<LongRange, LongRange> Ranges = [];

    public Mapping() { }

    /// <summary>
    /// Transform the given value through the map.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public long MappingFor(long value) {
        foreach (var range in Ranges) {
            // if in 'source' range
            if (range.Key.Contains(value)) {
                // find value from 'destination' range
                return range.Value.ValueOf(range.Key.IndexOf(value));
            }
        }
        // by default, return itself
        return value;
    }
}

class Day05: ISolution {
    public static void Solve() {
        // process input
        var input = GetInput("05");
        var initialSeeds = input[0].Split(": ")[1].Split(" ").Select(long.Parse).ToList();

        // map reading:
        // destinationStart sourceStart rangeLength

        var seedToSoilMap = new Mapping();
        for (var l = 3; l < 32; ++l) {
            var split = input[l].Split(' ').Select(long.Parse).ToList();
            var sourceRange = new Mapping.LongRange(split[1], split[1] + split[2]);
            var destinationRange = new Mapping.LongRange(split[0], split[0] + split[2]);
            seedToSoilMap.Ranges.Add(sourceRange, destinationRange);
        }
        var soilToFertiliserMap = new Mapping();
        for (var l = 34; l < 53; ++l) {
            var split = input[l].Split(' ').Select(long.Parse).ToList();
            var sourceRange = new Mapping.LongRange(split[1], split[1] + split[2]);
            var destinationRange = new Mapping.LongRange(split[0], split[0] + split[2]);
            soilToFertiliserMap.Ranges.Add(sourceRange, destinationRange);
        }
        var fertiliserToWaterMap = new Mapping();
        for (var l = 55; l < 97; ++l) {
            var split = input[l].Split(' ').Select(long.Parse).ToList();
            var sourceRange = new Mapping.LongRange(split[1], split[1] + split[2]);
            var destinationRange = new Mapping.LongRange(split[0], split[0] + split[2]);
            fertiliserToWaterMap.Ranges.Add(sourceRange, destinationRange);
        }
        var waterToLightMap = new Mapping();
        for (var l = 99; l < 116; ++l) {
            var split = input[l].Split(' ').Select(long.Parse).ToList();
            var sourceRange = new Mapping.LongRange(split[1], split[1] + split[2]);
            var destinationRange = new Mapping.LongRange(split[0], split[0] + split[2]);
            waterToLightMap.Ranges.Add(sourceRange, destinationRange);
        }
        var lightToTemperatureMap = new Mapping();
        for (var l = 119; l < 132; ++l) {
            var split = input[l].Split(' ').Select(long.Parse).ToList();
            var sourceRange = new Mapping.LongRange(split[1], split[1] + split[2]);
            var destinationRange = new Mapping.LongRange(split[0], split[0] + split[2]);
            lightToTemperatureMap.Ranges.Add(sourceRange, destinationRange);
        }
        var temperatureToHumidityMap = new Mapping();
        for (var l = 134; l < 144; ++l) {
            var split = input[l].Split(' ').Select(long.Parse).ToList();
            var sourceRange = new Mapping.LongRange(split[1], split[1] + split[2]);
            var destinationRange = new Mapping.LongRange(split[0], split[0] + split[2]);
            temperatureToHumidityMap.Ranges.Add(sourceRange, destinationRange);
        }
        var humidityToLocationMap = new Mapping();
        for (var l = 146; l < 190; ++l) {
            var split = input[l].Split(' ').Select(long.Parse).ToList();
            var sourceRange = new Mapping.LongRange(split[1], split[1] + split[2]);
            var destinationRange = new Mapping.LongRange(split[0], split[0] + split[2]);
            humidityToLocationMap.Ranges.Add(sourceRange, destinationRange);
        }

        // PART 1 - use the maps on the basic seed list
        var answer1 = long.MaxValue;
        foreach (var seed in initialSeeds) {
            // follow this number through each map until we reach the end
            var soil = seedToSoilMap.MappingFor(seed);
            var fertiliser = soilToFertiliserMap.MappingFor(soil);
            var water = fertiliserToWaterMap.MappingFor(fertiliser);
            var light = waterToLightMap.MappingFor(water);
            var temperature = lightToTemperatureMap.MappingFor(light);
            var humidity = temperatureToHumidityMap.MappingFor(temperature);
            var location = humidityToLocationMap.MappingFor(humidity);

            if (location < answer1) {
                answer1 = location;
            }
        }
        Console.WriteLine($"Part 1: {answer1}");

        // PART 2 - use the maps on the advanced seed list
        var answer2 = long.MaxValue;
        // each [start, length] pair of 'seed' numbers defines a range
        for (var idx = 0; idx < initialSeeds.Count; idx += 2) {
            // for each seed number in the range
            for (var seed = initialSeeds[idx]; seed < initialSeeds[idx] + initialSeeds[idx + 1]; ++seed) {
                // follow this number through each map until we reach the end
                var soil = seedToSoilMap.MappingFor(seed);
                var fertiliser = soilToFertiliserMap.MappingFor(soil);
                var water = fertiliserToWaterMap.MappingFor(fertiliser);
                var light = waterToLightMap.MappingFor(water);
                var temperature = lightToTemperatureMap.MappingFor(light);
                var humidity = temperatureToHumidityMap.MappingFor(temperature);
                var location = humidityToLocationMap.MappingFor(humidity);

                if (location < answer2) {
                    answer2 = location;
                }
                if (seed % 1000000 == 0) {
                    Console.Write($"{initialSeeds[idx]} / {seed} / {initialSeeds[idx] + initialSeeds[idx + 1]}\r");
                }
            }
            Console.WriteLine($"{idx / 2} / {initialSeeds.Count / 2}");
        }
        Console.WriteLine($"Part 2: {answer2}");
    }
}
