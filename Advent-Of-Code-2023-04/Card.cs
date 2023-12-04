namespace AdventOfCode.Day04
{
    public class Card
    {
        public int CardId { get; private set; }
        public List<int> GameNumbers { get; private set; } = [];
        public List<int> WinningNumbers { get; private set; } = [];
        public List<int> WinningGameNumbers => GetWinningGameNumbers();

        public Card(string input)
        {
            string[] splitInput = input.Split(':');
            string[] cardInfo = splitInput[0].Trim(' ').Split(' ');
            string[] cardNumbers = splitInput[1].Split('|');
            string[] gameNumbers = cardNumbers[0].Trim(' ').Split(' ');
            string[] winningNumbers = cardNumbers[1].Trim(' ').Split(' ');

            CardId = int.Parse(cardInfo[^1]);
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

        public List<int> GetWinningGameNumbers()
        {
            List<int> winningNumbers = [];
            foreach (int gameNumber in GameNumbers)
            {
                if (WinningNumbers.Contains(gameNumber))
                    winningNumbers.Add(gameNumber);
            }
            return winningNumbers;
        }
    }
}