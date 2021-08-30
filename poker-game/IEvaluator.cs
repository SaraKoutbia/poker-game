namespace poker_game
{
    interface IEvaluator
    {
        void EvaluateCards();
        Score GetCurrentScore();
    }
}