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
        public  List<Card> CurrentCards { set { _currentCards = value; } get { return _currentCards; } }

        public Player(ILogger<Player> log, IConfiguration config)
        {
            _log = log;
            _config = config;
            CurrentCards = new List<Card>();

        }

        Score IPlayer.EvaluateCards()
        {
            throw new System.NotImplementedException();
        }

        void IPlayer.PrintCards()
        {
            throw new System.NotImplementedException();
        }
    }
}