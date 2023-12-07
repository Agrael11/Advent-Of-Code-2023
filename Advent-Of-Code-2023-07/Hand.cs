namespace AdventOfCode.Day07
{
    /// <summary>
    /// Represents Hand of 5 cards
    /// </summary>
    public class Hand
    {
        // Card Types (In ascending value)
        public enum CardType { HighCard = 0, OnePair = 1, TwoPair = 2, ThreeOfAKind = 3, FullHouse = 4, FourOfAKind = 5, FiveOfAKind = 6 }

        //Table of values assigned to cards
        readonly Dictionary<char, int> ValuesTable = new() {
            { '2', 2 },
            { '3', 3 },
            { '4', 4 },
            { '5', 5 },
            { '6', 6 },
            { '7', 7 },
            { '8', 8 },
            { '9', 9 },
            { 'T', 10 },
            { 'J', 11 },
            { 'Q', 12 },
            { 'K', 13 },
            { 'A', 14 } };

        //Just parsed values of  cards using the table
        public readonly List<int> CardValues = [];
        //Type of hand
        public CardType HandType { get; private set; }
        //Bid of hand
        public int Bid { get; private set; }

        public Hand(string handInput)
        {
            string[] handInputs = handInput.Split(' ');
            //Bid is second value on hand input info
            Bid = int.Parse(handInputs[1]);

            //This will convert card to list of values
            string cardsData = handInputs[0];
            List<char> cards = [.. cardsData.ToCharArray()];
            foreach (char card in cards)
            {
                CardValues.Add(ValuesTable[card]);
            }

            //And also gets hand type
            HandType = GetHandType(cards);
        }

        /// <summary>
        /// Get hand type depending on cards we have
        /// </summary>
        /// <param name="cards"></param>
        /// <returns></returns>
        public static CardType GetHandType(List<char> cards)
        {
            if (ListCount(ref cards, cards[0]) == 5) return CardType.FiveOfAKind; //If 5 same cards then we have 5 of a kind
            if (ListCount(ref cards, cards[0]) == 4) return CardType.FourOfAKind; //If 4 same cards
            if (ListCount(ref cards, cards[1]) == 4) return CardType.FourOfAKind; //Then we have four of a kind
            //We check this first, as if this is true, none other possibilities can't be

            //Remembers how many doubles or triples we found, also what cards we checked so we don't double-count
            int doubles = 0;
            int triples = 0;
            List<char> checkedCards = [];

            //Checks every card for number of repeats on hand
            foreach (char card in cards)
            {
                if (checkedCards.Contains(card)) continue; //If we already checked the card ignore

                int count = ListCount(ref cards, card); //Number of repeats

                if (count == 3) triples++; //If 3 we add it as a triple
                else if (count == 2) doubles++; //If 2 we add it as a double
                
                checkedCards.Add(card); //And we remember to not check this card again
            }

            //Just checks for combinations and returns them - in order of their value
            if (doubles == 1 && triples == 1) return CardType.FullHouse;
            if (triples == 1) return CardType.ThreeOfAKind;
            if (doubles == 2) return CardType.TwoPair;
            if (doubles == 1) return CardType.OnePair;
            return CardType.HighCard;
        }

        /// <summary>
        /// Counts number of appearances of card on hand
        /// </summary>
        /// <param name="handInfo"></param>
        /// <param name="card"></param>
        /// <returns></returns>
        public static int ListCount(ref List<char> handInfo, char card)
        {
            return handInfo.Where((x) => (x == card)).Count();
        }

        /// <summary>
        /// Compare function - compares whether hand1 is higher (1) than card 2, equal (0) or smaller (-1).
        /// </summary>
        /// <param name="hand1"></param>
        /// <param name="hand2"></param>
        /// <returns></returns>
        public static int Compare(Hand hand1, Hand hand2)
        {
            if ((int)hand1.HandType > (int)hand2.HandType) return 1;
            if ((int)hand1.HandType == (int)hand2.HandType)
            {
                for (int i = 0; i < 5; i++)
                {
                    if (hand1.CardValues[i] > hand2.CardValues[i]) return 1;
                    else if (hand1.CardValues[i] < hand2.CardValues[i]) return -1;
                }
                return 0;
            }
            return -1;
        }
    }
}