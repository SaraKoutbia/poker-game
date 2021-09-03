﻿using System.Collections.Generic;

namespace poker_game
{
    internal interface IPlayer
    {
        List<Card> CurrentCards { get; set; }
        CurrentRoundResults EvaluateCards();
        void PrintCurrentScore();
        void PrintTotal();
        string Name { get; set; }
        Score Score { get; set; }
    }
}