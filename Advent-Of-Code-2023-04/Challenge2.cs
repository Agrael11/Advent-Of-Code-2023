namespace AdventOfCode.Day04
{
    /// <summary>
    /// Main Class for Challenge 2
    /// </summary>
    public static class Challenge2
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

            int totalCards = 0;
            List<(int cardCount, Card card)> cards = [];

            foreach (string cardInput in inputData)
            {
                cards.Add((1, new(cardInput)));
            }

            for (int i = 0; i < cards.Count; i++)
            {
                (int cardCount, Card card) = cards[i];
                int wins = card.WinningGameNumbers.Count;
                for (int j = i+1; j < cards.Count && j <= i+wins; j++)
                {
                    var newCard = cards[j];
                    newCard.cardCount += cardCount;
                    cards[j] = newCard;
                }
                totalCards += cardCount;
            }

            return totalCards;
        }
    }
}