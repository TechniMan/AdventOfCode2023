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

        #region create the maps
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
        #endregion

        // now use the maps
        var lowestLocation = long.MaxValue;
        foreach (var seed in initialSeeds) {
            // follow this number through each map until we reach the end
            var soil = seedToSoilMap.MappingFor(seed);
            var fertiliser = soilToFertiliserMap.MappingFor(soil);
            var water = fertiliserToWaterMap.MappingFor(fertiliser);
            var light = waterToLightMap.MappingFor(water);
            var temperature = lightToTemperatureMap.MappingFor(light);
            var humidity = temperatureToHumidityMap.MappingFor(temperature);
            var location = humidityToLocationMap.MappingFor(humidity);

            if (location < lowestLocation) {
                lowestLocation = location;
            }
        }

        // find answer
        var answer1 = lowestLocation;
        Console.WriteLine($"Part 1: {answer1}");
    }
}
