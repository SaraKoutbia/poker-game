using System;
using System.Collections.Generic;
using System.Text;
using static poker_game.Enums;

namespace poker_game
{
    internal class Card
    {
        internal Suit Suit { get; set; }
        internal int Rank { get; set; }

        //TODO: delete
        internal int Position { get; set; }
    }
}
