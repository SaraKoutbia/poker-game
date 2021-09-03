using System.Collections.Generic;

namespace poker_game
{
    internal interface IPlayer
    {
        List<Card> CurrentCards { get; set; }
        CurrentRoundResults EvaluateCards();
        void PrintCards();
        string Name { get; set; }
    }
}