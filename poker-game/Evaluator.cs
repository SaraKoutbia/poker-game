using System;
using System.Collections.Generic;
using System.Linq;
using static poker_game.Enums;

namespace poker_game
{
    class Evaluator : IEvaluator
    {
        internal List<Card> Cards = new List<Card>();
        internal Dictionary<int, int> rankDic;
        internal Dictionary<Suit, int> suitDic;
        internal CurrentRoundResults score;

        public Evaluator()
        {
            score = new CurrentRoundResults { _category = Category.None, _highestRank = 0 };
            rankDic = new Dictionary<int, int>();
            suitDic = new Dictionary<Suit, int>();
        }

        private void Initialize(List<Card> cards)
        {
            rankDic = new Dictionary<int, int>();
            suitDic = new Dictionary<Suit, int>();
            Cards = cards;
            EvaluateCards();
        }

        private void EvaluateCards()
        {
            foreach (var Card in Cards)
            {
                if (!rankDic.ContainsKey(Card.Rank))
                    rankDic[Card.Rank] = 1;
                else
                {
                    rankDic[Card.Rank]++;
                }
                if (!suitDic.ContainsKey(Card.Suit))
                    suitDic[Card.Suit] = 1;
                else
                {
                    suitDic[Card.Suit]++;
                }
            }
        }

        public CurrentRoundResults GetCurrentScore(List<Card> cards)
        {
            Initialize(cards);
            var suitMaxVal = suitDic.Values.Max();
            score._highestRank = rankDic.Keys.Max();
            score._2ndHighestRank = rankDic.Keys.Where(x => x != score._highestRank).Max();
            switch (suitMaxVal)
            {
                case 5://flush + StraightFlush
                    if ((score._highestRank - rankDic.Values.Min()) == 4)//Five cards in sequence
                        score._category = Category.StraightFlush;
                    else
                        score._category = Category.Flush;//cannot be FourOfAKind nor FullHouse
                    break;

                default:
                    score._highestRank = rankDic.Where(x => x.Value == rankDic.Values.Max()).Max(x => x.Key);
                    score._2ndHighestRank = rankDic.Keys.Where(x => x != score._highestRank).Max();
                    switch (rankDic.Values.Max())
                    {
                        case 4://FourOfAKind
                            score._category = Category.FourOfAKind;
                            //TODO
                            score._highestRank = rankDic.First(x => x.Value == rankDic.Values.Max()).Value;
                            score._2ndHighestRank = rankDic.Keys.Where(x => x != score._highestRank).Max();
                            break;
                        case 3: //FullHouse + ThreeOfAKind
                            if (rankDic.Values.Count(x => x > 1) == 2)
                                score._category = Category.FullHouse;
                            else
                                score._category = Category.ThreeOfAKind;
                            break;
                        case 2://TwoPair
                            if (rankDic.Values.Count(x => x > 1) == 2)
                                score._category = Category.TwoPair;
                            else
                                score._category = Category.OnePair;
                            break;
                        default:
                            if ((score._highestRank - rankDic.Values.Min()) == 4)//Five cards in sequence
                                score._category = Category.Straight;
                            else
                                score._category = Category.HighCard;
                            break;
                    }
                    break;
            }
            return score;
        }
    }
}
