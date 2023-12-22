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
        private static Dictionary<List<Brick>, int> memory = [];
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

            memory = new Dictionary<List<Brick>, int>(new ListComparer<Brick>());

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

            foreach (Brick brick in bricks)
            {
                foreach (Brick brick2 in bricks)
                {
                    if (brick.Equals(brick2)) continue;
                    if (brick.Intersects(brick2)) throw new Exception("Oh");
                }
            }

            int disintegrated = 0;

            string bricksResult = "";
            string brickst = "";

            foreach (Brick brick in bricks)
            {
                
                disintegrated += Disintegrates([brick]);
            }

            return disintegrated;
        }

        private  static string BrickToString(Brick brick)
        {
            return "v1_" + brick.Position.X + "_" + brick.Position.Y + "_" + brick.Position.Z + "_v2_"
                    + brick.Position2.X + "_" + brick.Position2.Y + "_" + brick.Position2.Z;
        }

        private static int Disintegrates(List<Brick> bricks)
        {
            if (memory.TryGetValue(bricks, out int value))
            {
                return value;
            }

            int disintegrate = 0;
            List<Brick> toDisintegrate = [];
            foreach (Brick suporter in bricks)
            {
                foreach (Brick supported in suporter.Supports)
                {
                    if (!toDisintegrate.Contains(supported))
                    {
                        List<Brick> supporting = [];
                        supporting.AddRange(supported.SupportedBy);
                        foreach (Brick supporter2 in bricks)
                        {
                            supporting.Remove(supporter2);
                        }
                        if (supporting.Count == 0)
                        {
                            toDisintegrate.Add(supported);
                        }
                    }
                }
            }

            disintegrate = toDisintegrate.Count;

            if (toDisintegrate.Count > 0)
            {
                disintegrate += Disintegrates(toDisintegrate);
            }

            memory.Add(bricks, disintegrate);
            return disintegrate;
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