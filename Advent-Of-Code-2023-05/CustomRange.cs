using System.Runtime.ExceptionServices;

namespace AdventOfCode.Day05
{

    /// <summary>
    /// This class takes care of ranges (with some nice bonuses over standard Range class)
    /// </summary>
    public class CustomRange
    {
        private ulong _start;
        private ulong _end;
        public ulong Start 
        {
            get
            { 
                return _start; 
            }
            set
            {
                _start = value;
                if (End < _start) End = Start;
            }
        }

        public ulong End 
        { 
            get
            {
                return _end;
            }
            set
            {
                if (value < Start) _end = Start;
                else _end = value;
            }
        }

        public CustomRange(ulong start, ulong end)
        {
            Start = start;
            End = end;
        }

        /// <summary>
        /// Checks if value is in Range
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool InRange(ulong value, CustomRange range)
        {
            return (value >= range.Start) && (value <= range.End);
        }

        /// <summary>
        /// Map value by range1->range2
        /// </summary>
        /// <param name="value"></param>
        /// <param name="otherRange"></param>
        /// <returns></returns>
        public static ulong Map(ulong value, CustomRange range1, CustomRange range2)
        {
            return range2.Start + value - range1.Start;
        }

        /// <summary>
        /// Converts into list of numbers
        /// </summary>
        /// <returns></returns>
        public List<ulong> ToList()
        {
            List<ulong> output = [];
            for (ulong i = Start; i<= End; i++)
            {
                output.Add(i);
            }
            return output;
        }

        /// <summary>
        /// Tries to combine two ranges into one
        /// </summary>
        /// <param name="range1"></param>
        /// <param name="range2"></param>
        /// <returns></returns>
        public static CustomRange? TryCombine(CustomRange range1, CustomRange range2)
        {
            if (range1.Start <= range2.Start)
            {
                if (range1.End < range2.Start) return null;
                if (range1.End >= range2.End) return range1;
                return new (range1.Start, range2.End);
            }
            else
            {
                if (range2.End < range1.Start) return null;
                if (range2.End >= range1.End) return range2;
                return new (range2.Start, range1.End);
            }
        }

        /// <summary>
        /// Sorts List of ranges by Start value
        /// </summary>
        /// <param name="ranges"></param>
        /// <returns></returns>
        public static List<CustomRange> Sort(List<CustomRange> ranges)
        {
            ranges = [.. ranges.OrderBy((range) => { return range.Start; })];

            return ranges;
        }

        /// <summary>
        /// Tries to combine list of ranges into smaller list
        /// </summary>
        /// <param name="ranges"></param>
        /// <returns></returns>
        public static List<CustomRange> TryCombine(List<CustomRange> ranges)
        {
            List<CustomRange> targetRanges = Sort(ranges);
            if (targetRanges.Count > 1)
            {
                for (int i = 0; i < targetRanges.Count; i++)
                {
                    for (int j = i + 1; j < targetRanges.Count; j++)
                    {
                        CustomRange? range = TryCombine(targetRanges[i], targetRanges[j]);
                        if (range == null) continue;
                        targetRanges[i] = range;
                        targetRanges.RemoveAt(j);
                        i--;
                        break;
                    }
                }
            }
            return targetRanges;
        }

        /// <summary>
        /// Gets overlap between two ranges
        /// </summary>
        /// <param name="range1"></param>
        /// <param name="range2"></param>
        /// <returns></returns>
        public static CustomRange? GetOverlap(CustomRange range1, CustomRange range2)
        {
            if (range2.Start > range1.End || range2.End < range1.Start) return null;
            
            ulong start = range2.Start;
            if (start < range1.Start) start = range1.Start;
            ulong end = range2.End;
            if (end > range1.End) end = range1.End;

            return new(start, end);
        }

        /// <summary>
        /// Splits Range by second Range
        /// </summary>
        /// <param name="inputRange1"></param>
        /// <param name="inputRange2"></param>
        /// <returns></returns>
        public static (CustomRange? startRange, CustomRange? mainRange, CustomRange? endRange) SplitByRange(CustomRange inputRange1,CustomRange inputRange2)
        {
            CustomRange? range2 = GetOverlap(inputRange1, inputRange2);
            if (range2 == null)
            {
                if (inputRange2.Start > inputRange1.Start) return (new CustomRange(inputRange1.Start, inputRange1.End), null, null);
                return (null, null, new CustomRange(inputRange1.Start, inputRange1.End));
            }

            CustomRange? range1 = null;
            if (range2.Start > inputRange1.Start) range1 = new CustomRange(inputRange1.Start, range2.Start - 1);

            CustomRange? range3 = null;
            if (range2.End < inputRange1.End) range3 = new CustomRange(range2.End + 1, inputRange1.End);

            return (range1, range2, range3);
        }

        /// <summary>
        /// Debug help
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{Start}..{End}";
        }
    }
}
