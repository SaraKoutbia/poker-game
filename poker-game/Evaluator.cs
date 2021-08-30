using System;
using System.Collections.Generic;
using System.Text;
using static poker_game.Enums;

namespace poker_game
{
    class Evaluator : IEvaluator
    {
        internal List<Card> Cards = new List<Card>();
        internal Dictionary<int, int> rankDic;
        internal Dictionary<Suit, int> suitDic;
        internal Score score;

        public Evaluator(List<Card> cards)
        {
            Cards = cards;
            score = new Score { _currentCategory = Category.None, _currentHighestRank = 0 };
            rankDic = new Dictionary<int, int>();
            suitDic = new Dictionary<Suit, int>();
            EvaluateCards();
        }

        public void EvaluateCards()
        {
        }

        public Score GetCurrentScore()
        {
            //...
            return score;
        }
    }
}
