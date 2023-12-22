using Advent_Of_Code_2023_22;
using Helpers;
using System.Numerics;

namespace AdventOfCode.Day22
{
    /// <summary>
    /// Main Class for Challenge 2
    /// </summary>
    public static class Challenge2
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


            bricks = [.. bricks.OrderBy((brick) => { return brick.Position.Z; })];
            while (MoveBricksDown()) ;

            foreach (Brick brick in bricks)
            {
                foreach (Brick supportingBrick in brick.SupportedBy)
                {
                    supportingBrick.Supports.Add(brick);
                }
            }
            
            int disintegrated = 0;

            //checks how many bricks would break if we removed any of them.
            foreach (Brick brick in bricks)
            {
                disintegrated += Disintegrates([brick]);
            }

            return disintegrated;
        }

        private static string BrickToString(Brick brick)
        {
            return "v1_" + brick.Position.X + "_" + brick.Position.Y + "_" + brick.Position.Z + "_v2_"
                    + brick.Position2.X + "_" + brick.Position2.Y + "_" + brick.Position2.Z;
        }

        /// <summary>
        /// Count how  many bricks can would break if "removedBricks" was removed.
        /// It also passes those bricks and new brinks that would break to next attempt
        /// </summary>
        /// <param name="removedBricks"></param>
        /// <returns></returns>
        private static int Disintegrates(List<Brick> removedBricks)
        {
            int disintegrate = 0;
            List<Brick> toDisintegrate = [];
            foreach (Brick supporter in removedBricks)
            {
                foreach (Brick supported in supporter.Supports)
                {
                    if (!removedBricks.Contains(supported) && !toDisintegrate.Contains(supported))
                    {
                        toDisintegrate.Add(supported);
                    }
                }
            }
            for (int i = toDisintegrate.Count-1; i >= 0; i--)
            {
                List<Brick> supportedBy = [.. toDisintegrate[i].SupportedBy];
                foreach (Brick supporter in removedBricks)
                {
                    supportedBy.Remove(supporter);
                }
                if (supportedBy.Count > 0)
                {
                    toDisintegrate.RemoveAt(i);
                }
            }

            disintegrate = toDisintegrate.Count;

            if (toDisintegrate.Count > 0)
            {
                toDisintegrate.AddRange(removedBricks);
                disintegrate += Disintegrates(toDisintegrate);
            }

            return disintegrate;
        }

        /// <summary>
        /// Tries to push all bricks down
        /// </summary>
        /// <returns></returns>
        private static bool MoveBricksDown()
        {
            bool moved = false;
            for (int i = 0; i < bricks.Count; i++)
            {
                moved |= MoveBrickDown(i);
            }
            return moved;
        }

        /// <summary>
        /// Tries to push one brick down.
        /// </summary>
        /// <param name="brickIndex"></param>
        /// <returns></returns>
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
                    //This part also makes brick remember on what bricks it's standing
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