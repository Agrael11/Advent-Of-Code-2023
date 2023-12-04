namespace AdventOfCode.Day04
{
    /// <summary>
    /// Takes care of single game card
    /// </summary>
    public class Card
    {
        public List<int> GameNumbers { get; private set; } = []; //Contains Numbers on Game
        public List<int> WinningNumbers { get; private set; } = []; //Contains Winning numbers
        public int WinningGameNumbersCount => GetWinningGameNumbersCount(); //Just shortcut to a function

        public Card(string input)
        {
            //I split information from card for parsing
            string[] splitInput = input.Split(':');
            string[] cardNumbers = splitInput[1].Split('|');
            string[] gameNumbers = cardNumbers[0].Trim(' ').Split(' ');
            string[] winningNumbers = cardNumbers[1].Trim(' ').Split(' ');

            //This adds numbers from cards, and ensures no whitespace that is there.
            foreach (string gameNumber in gameNumbers)
            {
                if (string.IsNullOrWhiteSpace(gameNumber)) continue;

                GameNumbers.Add(int.Parse(gameNumber));
            }
            foreach (string winningNumber in winningNumbers)
            {
                if (string.IsNullOrWhiteSpace(winningNumber)) continue;

                WinningNumbers.Add(int.Parse(winningNumber));
            }
        }

        /// <summary>
        /// Gets winning numbers count
        /// </summary>
        /// <returns></returns>
        public int GetWinningGameNumbersCount()
        {
            int count = 0;
            foreach (int gameNumber in GameNumbers)
            {
                if (WinningNumbers.Contains(gameNumber))
                    count++;
            }
            return count;
        }
    }
}