using System.Drawing;
using System.Windows.Markup;

namespace AdventOfCode.Day18
{
    /// <summary>
    /// Main Class for Challenge 2
    /// </summary>
    public static class Challenge2
    {
        static readonly Dictionary<long, Dictionary<long, long>> Vertices = [];

        static readonly long right = 1;
        static readonly long bottom = 1;
        static readonly long top = 0;
        static readonly long left = 0;

        /// <summary>
        /// This is the Main function
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        public static long DoChallenge(string input)
        {
            //Read input data
            string[] inputData = input.Replace("\r", "").TrimEnd('\n').Split('\n');

            (long x, long y) = (0, 0);
            List<(long x, long y)> polygonVertices = [];

            //Parse input into list of connected vertices
            foreach (string inputLine in inputData)
            {
                string[] data = inputLine.Split();
                long length = long.Parse(data[2][2..^2], System.Globalization.NumberStyles.HexNumber);
                /*
                 * Me: "Oh, this is easy to convert for part 2"
                 * Also me: "Why is this wrong? Let's see if i parse everything right... this is not right... ooooh last digit is now instruction!"
                 */
                switch (data[2][^2])
                {
                    case '3':
                        polygonVertices.Add((x, y));
                        y -= length;
                        polygonVertices.Add((x, y));
                        break;
                    case '1':
                        polygonVertices.Add((x, y));
                        y += length;
                        polygonVertices.Add((x, y));
                        break;
                    case '0':
                        x += length;
                        break;
                    case '2':
                        x -= length;
                        break;
                }
            }

            long total = 0;

            //Shoelace
            for (int i = 0; i < polygonVertices.Count; i++)
            {
                long x1 = polygonVertices[i].x;
                long y1 = polygonVertices[i].y;
                long x2 = 0;
                long y2 = 0;
                if (i + 1 == polygonVertices.Count)
                {
                    x2 = polygonVertices[0].x;
                    y2 = polygonVertices[0].y;
                }
                else
                {
                    x2 = polygonVertices[i + 1].x;
                    y2 = polygonVertices[i + 1].y;
                }
                total += (x1 * y2 - x2 * y1);

                //Add sides themselves
                total += Math.Abs((x2 - x1)) + Math.Abs(y2 - y1);
            }

            //Add 1?
            return total / 2 + 1;
        }
    }
}