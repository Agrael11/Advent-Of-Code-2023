using AdventOfCode.Day05;
using System.Numerics;
using System.Security.Cryptography;

namespace AdventOfCode.Day05
{
    /// <summary>
    /// One mapping in Almanac
    /// </summary>
    public class AlmanacMap
    {
        public readonly List<(CustomRange sourceRange, CustomRange destinationRange)> RangeMaps = [];
        public readonly string Source;
        public readonly string Destination;

        public AlmanacMap(string[] dataSource, int start, int end)
        {
            string[] mapNames = dataSource[start].Split(' ')[0].Split('-');
            Source = mapNames[0];
            Destination = mapNames[2];
            for (int i =  start + 1; i < end; i++)
            {
                RangeMaps.Add(GenerateRangeMap(dataSource[i]));
            }
            RangeMaps = [.. RangeMaps.OrderBy((map) => { return map.sourceRange.Start; })];
        }

        /// <summary>
        /// Generates map of ranges (source->destination) by input string
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private static (CustomRange rangeSource, CustomRange rangeDestination) GenerateRangeMap(string input)
        {
            string[] rangeDefinitions = input.Split(' ');
            ulong destinationStart = ulong.Parse(rangeDefinitions[0]);
            ulong sourceStart = ulong.Parse(rangeDefinitions[1]);
            ulong rangeLength = ulong.Parse(rangeDefinitions[2]) - 1;
            CustomRange rangeDestination = new(destinationStart, destinationStart + rangeLength);
            CustomRange rangeSource = new(sourceStart, sourceStart + rangeLength);
            return (rangeSource, rangeDestination);
        }

        /// <summary>
        /// Maps number by mappings
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public ulong GetMapped(ulong source)
        {
            foreach ((CustomRange sourceRange, CustomRange destinationRange) in RangeMaps)
            {
                if (CustomRange.InRange(source, sourceRange))
                {
                    return CustomRange.Map(source, sourceRange, destinationRange);
                }
            }

            return source;
        }

        /// <summary>
        /// Maps range into multiple ranges
        /// </summary>
        /// <param name="range">Input range</param>
        /// <returns></returns>
        public List<CustomRange> MapRange(CustomRange range)
        {
            List<CustomRange> ranges = [range];

            foreach ((CustomRange sourceRange, CustomRange destinationRange) in RangeMaps)
            {
                CustomRange workRange = ranges[^1];
                ranges.RemoveAt(ranges.Count - 1);

                var (preRange, mainRange, endRange) = CustomRange.SplitByRange(workRange, sourceRange);
                
                if (preRange != null) ranges.Add(preRange);
                if (mainRange != null)
                {
                    ulong offset = destinationRange.Start - sourceRange.Start;
                    mainRange = new CustomRange(mainRange.Start + offset, mainRange.End + offset);
                    ranges.Add(mainRange);
                }
                if (endRange != null) ranges.Add(endRange);
                else break;
            }

            return ranges;
        }

        /// <summary>
        /// Generates list of Almanac Mappings, from string. Basic parse to create this class list
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        public static Dictionary<string, AlmanacMap> GenerateMaps(string[] inputData)
        {
            Dictionary<string, AlmanacMap> maps = [];

            AlmanacMap map;
            int startRange = -1;
            for (int lineIndex = 1; lineIndex < inputData.Length; lineIndex++)
            {
                if (string.IsNullOrWhiteSpace(inputData[lineIndex]))
                {
                    if (startRange == -1) continue;

                    map = new(inputData, startRange, lineIndex);
                    maps.Add(map.Source, map);
                    startRange = -1;
                    continue;
                }

                if (startRange == -1)
                {
                    startRange = lineIndex;
                }
            }

            if (startRange == -1) return maps;

            map = new(inputData, startRange, inputData.Length);
            maps.Add(map.Source, map);

            return maps;
        }
    }
}