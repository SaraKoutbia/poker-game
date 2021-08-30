using System.Collections.Generic;

namespace poker_game
{
    internal interface IPlayer
    {
        List<Card> CurrentCards { get; set; }
        Score EvaluateCards();
        void PrintCards();
    }
}