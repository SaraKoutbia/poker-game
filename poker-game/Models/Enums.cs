namespace poker_game
{
    class Enums
    {
        internal enum Suit
        {
            Clubs = 1,
            Diamonds = 2,
            Hearts = 3,
            Spades = 4

        }

        internal enum Category
        {
            StraightFlush = 1,//Five cards of the same suit in sequence (if those five are A, K, Q, J, 10; it is a Royal Flush)
            FourOfAKind = 2, //Four cards of the same rank and any one other card 
            FullHouse = 3,// Three cards of one rank and two of another 
            Flush = 4,// Five cards of the same suit
            Straight = 5,// Five cards in sequence (for example, 4, 5, 6, 7, 8) 
            ThreeOfAKind = 6,// Three cards of the same rank
            TwoPair = 7,// Two cards of one rank and two cards of another
            OnePair = 8,// Two cards of the same rank 
            HighCard = 9,// If no one has a pair, the highest card wins 
            None = 10
        }
    }
}
