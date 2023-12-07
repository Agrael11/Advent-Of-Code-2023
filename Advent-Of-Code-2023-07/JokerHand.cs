namespace AdventOfCode.Day07
{
    /// <summary>
    /// Represents Hand of 5 cards, but with Jokers!
    /// </summary>
    public class JokerHand
    {
        // Card Types (In ascending value)
        public enum CardType { HighCard = 0, OnePair = 1, TwoPair = 2, ThreeOfAKind = 3, FullHouse = 4, FourOfAKind = 5, FiveOfAKind = 6 }

        //Table of values assigned to cards, but J is joker - lowest value
        readonly Dictionary<char, int> ValuesTable = new() {
        { 'J', 1 },
        { '2', 2 },
        { '3', 3 },
        { '4', 4 },
        { '5', 5 },
        { '6', 6 },
        { '7', 7 },
        { '8', 8 },
        { '9', 9 },
        { 'T', 10 },
        { 'Q', 11 },
        { 'K', 12 },
        { 'A', 13 } };

        //Just parsed values of  cards using the table
        public readonly List<int> CardValues = [];
        //Type of hand
        public CardType HandType { get; private set; }
        //Bid of hand
        public int Bid { get; private set; }

        public JokerHand(string handInput)
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
            //Remembers how many doubles or triples we found, also what cards we checked so we don't double-count
            int doubles = 0;
            int triples = 0;
            List<char> checkedCards = [];

            //But also check for amount of jokers
            int jokers = ListCount(ref cards, 'J');

            //Checks every card for number of repeats on hand
            foreach (char card in cards)
            {
                int countWithJoker = ListCountWithJoker(ref cards, card); //Number of repeats with Joker

                if (countWithJoker == 5) return CardType.FiveOfAKind; //If 5 same cards
                if (countWithJoker == 4) return CardType.FourOfAKind; //If 4 same cards
                //We check for all card, because there can be 4 jokers and 1 other card and this way it's easier
                
                if (checkedCards.Contains(card)) continue;
                int count = ListCount(ref cards, card); //Number of repeats Without joker

                if (count == 3) triples++;
                else if (count == 2) doubles++;

                checkedCards.Add(card);
            }

            //Add jokers to best possibility
            if (jokers == 1)
            {
                if (triples == 0 && doubles == 0) doubles++;
                else if (triples == 0 && doubles >= 1) { doubles--; triples++; }
            }
            if (jokers == 2)
            {
                doubles = 0;
                triples = 1;
            }

            //Check for result
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
        /// Counts number of appearances of card on hand. But also checks for jokers
        /// </summary>
        /// <param name="handInfo"></param>
        /// <param name="card"></param>
        /// <returns></returns>
        public static int ListCountWithJoker(ref List<char> handInfo, char card)
        {
            return handInfo.Where((x) => (x == card || x=='J')).Count();
        }

        /// <summary>
        /// Compare function - compares whether hand1 is higher (1) than card 2, equal (0) or smaller (-1).
        /// </summary>
        /// <param name="hand1"></param>
        /// <param name="hand2"></param>
        /// <returns></returns>
        public static int Compare(JokerHand hand1, JokerHand hand2)
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