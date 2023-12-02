namespace AdventOfCode.Day02
{
    /// <summary>
    /// Main Class for Challenge 1
    /// </summary>
    public static class Challenge1
    {
        static readonly int _maxRed = 12;
        static readonly int _maxGreen = 13;
        static readonly int _maxBlue = 14;


        /// <summary>
        /// This is the Main function
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        public static int DoChallenge(string input)
        {
            //Read input data
            string[] inputData = input.Replace("\r", "").TrimEnd('\n').Split('\n');

            int validGames = 0;
            
            foreach (string gameInput in inputData) //For each game - one line of input
            {
                Game game = new(gameInput); //Generate game

                bool possible = true;
                foreach (Set set in game.Sets) //For each set in game
                {
                    if (set.Reds > _maxRed || set.Greens > _maxGreen || set.Blues > _maxBlue) //If any set is impossible set game as impossible
                    {
                        possible = false;
                        break;
                    }
                }
                if (possible) validGames += game.Id; //if no set is impossible, add gameId to sum of valid games;
            }

            return validGames;
        }
    }
}