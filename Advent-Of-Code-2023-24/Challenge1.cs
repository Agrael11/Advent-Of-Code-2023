using Helpers;


namespace AdventOfCode.Day24
{
    /// <summary>
    /// Main Class for Challenge 1
    /// </summary>
    public static class Challenge1
    {
        //Defines rectangle of check
        private static readonly double min = 200000000000000;
        private static readonly double max = 400000000000000;
        
        /// <summary>
        /// This is the Main function
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        public static long DoChallenge(string input)
        {
            //Read input data
            string[] inputData = input.Replace("\r", "").TrimEnd('\n').Split('\n');

            //Get list of hails
            List<(Vector2d position, Vector2d velocity)> hails = [];
            foreach (string inputLine in inputData)
            {
                string[] split = inputLine.Replace(" ", "").Split('@');
                string[] position = split[0].Split(',');
                string[] velocity = split[1].Split(',');
                double x1 = double.Parse(position[0]);
                double y1 = double.Parse(position[1]);
                double x2 = double.Parse(velocity[0]);
                double y2 = double.Parse(velocity[1]);
                hails.Add(new(new(x1,y1),new(x2,y2)));
            }

            //Check how many hails intersect within rectangle
            int crossTimes = 0;
            for (int i = 0; i < hails.Count; i++)
            {
                for (int j = i+1; j < hails.Count; j++)
                {
                    Vector2d? result = IntersectsWithOffset(hails[i].position, hails[i].velocity, hails[j].position, hails[j].velocity);
                    if (result is null)
                    {
                        continue;
                    }
                    if (result.X >= min && result.X <= max && result.Y >= min && result.Y <= max)
                    {
                        crossTimes++;
                    }
                }
            }


            return crossTimes;
        }

        /// <summary>
        /// Just inbetween function to convert offset into actual points
        /// </summary>
        /// <param name="line1Point"></param>
        /// <param name="line1Offset"></param>
        /// <param name="line2Point"></param>
        /// <param name="line2Offset"></param>
        /// <returns></returns>
        public static Vector2d? IntersectsWithOffset(Vector2d line1Point, Vector2d line1Offset, Vector2d line2Point, Vector2d line2Offset)
        {
            return Intersects(line1Point, new(line1Point.X + line1Offset.X, line1Point.Y + line1Offset.Y),
                line2Point, new(line2Point.X + line2Offset.X, line2Point.Y + line2Offset.Y));
        }

        /// <summary>
        /// Line-line intersection. easy
        /// </summary>
        /// <param name="line1Point1"></param>
        /// <param name="line1Point2"></param>
        /// <param name="line2Point1"></param>
        /// <param name="line2Point2"></param>
        /// <returns></returns>
        public static Vector2d? Intersects(Vector2d line1Point1, Vector2d line1Point2, Vector2d line2Point1, Vector2d line2Point2)
        {
            double x1 = line1Point1.X;
            double x2 = line1Point2.X;
            double x3 = line2Point1.X;
            double x4 = line2Point2.X;
            double y1 = line1Point1.Y;
            double y2 = line1Point2.Y;
            double y3 = line2Point1.Y;
            double y4 = line2Point2.Y;
            double D = (x1 - x2) * (y3 - y4) - (y1 - y2) * (x3 - x4);
            if (D == 0)
                return null;

            double t = ((x1 - x3) * (y3 - y4) - (y1 - y3) * (x3 - x4)) / D;
            double u = ((x1 - x3) * (y1 - y2) - (y1 - y3) * (x1 - x2)) / D;

            if (t < 0 || u < 0)
                return null;

            double Px = (x1 + t * (x2 - x1));
            double Py = (y1 + t * (y2 - y1));
            
            return new(Px, Py);
        }
    }
}