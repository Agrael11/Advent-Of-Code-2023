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
            
            int totalCards = 0; //Keeps total number of cards we have

            List<(int cardCount, Card card)> cards = [];

            foreach (string cardInput in inputData) //Just creates list of cards
            {
                cards.Add((1, new(cardInput)));
            }

            for (int i = 0; i < cards.Count; i++)
            {
                (int cardCount, Card card) = cards[i];
                totalCards += cardCount; //We add number of this card to total count

                //And for number of winning cards we add new next nth card for each of this card...
                //just look at the code, it's easier than comment xD
                for (int j = i+1; j < cards.Count && j <= i + card.WinningGameNumbersCount; j++)
                {
                    var newCard = cards[j];
                    newCard.cardCount += cardCount;
                    cards[j] = newCard;
                }
            }

            //result
            return totalCards;
        }
    }
}