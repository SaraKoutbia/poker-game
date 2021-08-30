using System.Collections.Generic;

namespace poker_game
{
    internal interface IPlayer
    {
        public List<Card> CurrentCards { get; set; }
        public Score EvaluateCards();
        public void PrintCards();
    }
}