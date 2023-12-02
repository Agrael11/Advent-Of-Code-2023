namespace AdventOfCode.Day02
{
    /// <summary>
    /// Main Class for Challenge 2
    /// </summary>
    public static partial class Challenge2
    {
        /// <summary>
        /// This is the Main function
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        public static int DoChallenge(string input)
        {
            //Read input data
            string[] inputData = input.Replace("\r", "").TrimEnd('\n').Split('\n');
            
            int powerOfGames = 0;

            foreach (string gameInput in inputData) //For each game in inputs
            {
                Game game = new(gameInput); //Create game from input line

                //Remember maximum of cubes per game
                int maximumRedsFound = 0;
                int maximumGreensFound = 0;
                int maximumBluesFound = 0;

                foreach (Set set in game.Sets) //For each set in game, we check if there's more of cubes we know about and if so replace maximum
                {
                    if (set.Reds > maximumRedsFound)
                    {
                        maximumRedsFound = set.Reds;
                    }
                    if (set.Greens > maximumGreensFound)
                    {
                        maximumGreensFound = set.Greens;
                    }
                    if (set.Blues > maximumBluesFound)
                    {
                        maximumBluesFound = set.Blues;
                    }
                }

                powerOfGames += maximumRedsFound * maximumGreensFound * maximumBluesFound; //add power of game to total
            }
            return powerOfGames;
        }
    }
}