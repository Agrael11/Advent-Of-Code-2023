using Advent_Of_Code_2023_22;
using System.Numerics;

namespace AdventOfCode.Day22
{
    /// <summary>
    /// Main Class for Challenge 1
    /// </summary>
    public static class Challenge1
    {
        private static List<Brick> bricks = [];

        /// <summary>
        /// This is the Main function
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        public static long DoChallenge(string input)
        {
            //Read input data
            string[] inputData = input.Replace("\r", "").TrimEnd('\n').Split('\n');

            bricks = [];

            //What is this parsing
            foreach (string inputLine in inputData)
            {
                string[] corners = inputLine.Split('~');
                string[] firstCorner = corners[0].Split(',');
                string[] secondCorner = corners[1].Split(',');
                int x1 = int.Parse(firstCorner[0]);
                int y1 = int.Parse(firstCorner[1]);
                int z1 = int.Parse(firstCorner[2]);
                int x2 = int.Parse(secondCorner[0]);
                int y2 = int.Parse(secondCorner[1]);
                int z2 = int.Parse(secondCorner[2]);
                bricks.Add(new(new(x1, y1, z1), new(x2, y2, z2)));
            }

            while (MoveBricksDown()) ;

            MoveBricksDown();

            foreach (Brick brick in bricks)
            {
                foreach(Brick supportingBrick in brick.SupportedBy)
                {
                    supportingBrick.Supports.Add(brick);
                }
            }

            int disintegrated = 0;
            foreach (Brick brick in bricks)
            {
                bool disintegrate = true;
                foreach (Brick supported in brick.Supports)
                {
                    if (supported.SupportedBy.Count <= 1) disintegrate = false;
                }
                if (disintegrate) disintegrated++;
            }

            return disintegrated;
        }

        private static bool MoveBricksDown()
        {
            bool moved = false;
            for (int i = 0; i < bricks.Count; i++)
            {
                moved |= MoveBrickDown(i);
            }
            return moved;
        }

        private static bool MoveBrickDown(int brickIndex)
        {
            Brick brick = bricks[brickIndex];
            brick.Position.Z--;
            brick.Position2.Z--;

            brick.SupportedBy.Clear();
            if (brick.Position.Z <= 0)
            {
                brick.Position.Z++;
                brick.Position2.Z++;
                return false;
            }

            bool moved = true;
            for (int i = 0; i < bricks.Count; i++)
            {
                if (i == brickIndex) continue;
                if (brick.Intersects(bricks[i]))
                {
                    brick.SupportedBy.Add(bricks[i]);
                    moved = false;
                }
            }

            if (!moved)
            {
                brick.Position.Z++;
                brick.Position2.Z++;
                return false;
            }

            return true;
        }
    }
}