using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace poker_game
{
    internal class Player : IPlayer
    {
        private readonly ILogger<Player> _log;
        private readonly IConfiguration _config;
        private List<Card> _currentCards;
        private string _name;
        internal Score _score;
        public List<Card> CurrentCards { set { _currentCards = value; } get { return _currentCards; } }

        public Score Score { set { _score = value; } get { return _score; } }
        public Player(ILogger<Player> log, IConfiguration config)
        {
            _log = log;
            _config = config;
            CurrentCards = new List<Card>();
            _score = new Score();
        }

        public string Name { set { _name = value; } get { return _name; } }

        public CurrentRoundResults EvaluateCards()
        {
            _score._currentRoundResults = Evaluator.GetCurrentScore(CurrentCards);
            return _score._currentRoundResults;
        }

        void IPlayer.PrintCurrentScore()
        {
            _log.LogInformation("{name} score: [category={p}, highest rank={r}, second highest rank={s}]", Name,
                _score._currentRoundResults._category, _score._currentRoundResults._highestRank, _score._currentRoundResults._2ndHighestRank);
        }

        void IPlayer.PrintTotal()
        {
            _log.LogInformation("{p} score: {score}", Name, _score._total);
        }
    }
}