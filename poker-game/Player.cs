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
        private IEvaluator _evaluator;
        private string _name;
        internal Score _score;
        public List<Card> CurrentCards { set { _currentCards = value; } get { return _currentCards; } }

        public Player(ILogger<Player> log, IConfiguration config, IEvaluator evaluator)
        {
            _log = log;
            _config = config;
            CurrentCards = new List<Card>();
            _evaluator = evaluator;//TODO should be static 
            _score = new Score();
        }

        public string Name { set { _name = value; } get { return _name; } }

        CurrentRoundResults IPlayer.EvaluateCards()
        {
            _score._currentRoundResults = _evaluator.GetCurrentScore(CurrentCards);
            return _score._currentRoundResults;
        }

        void IPlayer.PrintCards()
        {
            throw new System.NotImplementedException();
        }
    }
}