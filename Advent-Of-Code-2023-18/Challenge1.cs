using System.Reflection.Metadata;
using System.Runtime.ExceptionServices;
using System.Windows.Markup;

namespace AdventOfCode.Day18
{
    /// <summary>
    /// Main Class for Challenge 1
    /// </summary>
    public static class Challenge1
    {
        /// <summary>
        /// This is the Main function
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        public static long DoChallenge(string input)
        {
            //Read input data
            string[] inputData = input.Replace("\r", "").TrimEnd('\n').Split('\n');

            (long x, long y) currentPoint = (0, 0);
            List<(long x, long y)> polygonVertices = [];

            //Parse input to list of vertices
            foreach (string inputLine in inputData)
            {
                string[] data = inputLine.Split();
                char instruction = data[0][0];
                long length = long.Parse(data[1]);
                switch (instruction)
                {
                    case 'U':
                        polygonVertices.Add((currentPoint.x, currentPoint.y));
                        currentPoint.y -= length;
                        polygonVertices.Add((currentPoint.x, currentPoint.y));
                        break;
                    case 'D':
                        polygonVertices.Add((currentPoint.x, currentPoint.y));
                        currentPoint.y += length;
                        polygonVertices.Add((currentPoint.x, currentPoint.y));
                        break;
                    case 'R':
                        currentPoint.x += length;
                        break;
                    case 'L':
                        currentPoint.x -= length;
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

                //Add sides
                total += Math.Abs((x2 - x1)) + Math.Abs(y2 - y1);
            }

            //Off by 1?
            return total / 2 + 1;
        }
    }
}