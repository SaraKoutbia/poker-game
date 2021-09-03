using System.Collections.Generic;

namespace poker_game
{
    interface IEvaluator
    {
        CurrentRoundResults GetCurrentScore(List<Card> cards);
    }
}