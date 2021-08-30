using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace poker_game
{
    internal class Deck : IDeck
    {
        private readonly ILogger<PokerService> _log;
        private readonly IConfiguration _config;
        private List<Card> _cards = new List<Card>();
        internal int Count { get { return _cards.Count; } }

        int IDeck.Count => throw new NotImplementedException();

        private readonly int _cardsPerPlayerCount;
        public Deck(ILogger<PokerService> log, IConfiguration config)
        {
            _log = log;
            _config = config;
            _cardsPerPlayerCount = _config.GetSection("PokerRules:#CardsToServeEachPlayer").Get<int>();
        }

        void IDeck.Initialize()
        {
            throw new NotImplementedException();
        }

        void IDeck.Suffle()
        {
            throw new NotImplementedException();
        }

        bool IDeck.IsEmpty()
        {
            throw new NotImplementedException();
        }

        Card IDeck.Pop()
        {
            throw new NotImplementedException();
        }

        void IDeck.Print()
        {
            throw new NotImplementedException();
        }

        void IDeck.ServePlayer(IPlayer player)
        {
            throw new NotImplementedException();
        }
    }
}
