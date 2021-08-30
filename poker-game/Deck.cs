using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using static poker_game.Enums;

namespace poker_game
{
    internal class Deck : IDeck
    {
        private readonly ILogger<PokerService> _log;
        private readonly IConfiguration _config;
        private List<Card> _cards = new List<Card>();
        public int Count { get { return _cards.Count; } }
        private readonly int _cardsPerPlayerCount;
        private readonly int _numberCount;
        public Deck(ILogger<PokerService> log, IConfiguration config)
        {
            _log = log;
            _config = config;
            _cardsPerPlayerCount = _config.GetSection("PokerRules:#CardsToServeEachPlayer").Get<int>();
            _numberCount = _config.GetSection("PokerRules:#CardsPerSuit").Get<int>();
        }

        void IDeck.Initialize()
        {
            var suitsCount = Enum.GetNames(typeof(Suit)).Length;
           
            for (var i = 1; i <= suitsCount; i++)
            {
                for (var j = 1; j <= _numberCount; j++)
                {
                    _cards.Add(new Card { Suit = (Suit)i, Rank = j, Position = (i - 1) * 13 + j - 1 });
                }
            }
            _log.LogInformation("The Deck has been initialized. It contains {cardsCount} cards.", _cards.Count);
        }

        void IDeck.Suffle()
        {
            Random r = new Random();

            for (var i = 0; i < (_cards.Count); i++)
            {
                var jthPosition = r.Next(0, _cards.Count);
                var temCard = _cards[i];
                _cards[i] = _cards[jthPosition];
                _cards[jthPosition] = temCard;
            }
            _log.LogInformation("The Deck has been shuffled.");
        }

        bool IDeck.IsEmpty()
        {
            return _cards.Count == 0;
        }

        Card IDeck.Pop()
        {
            var lastCard = _cards[Count - 1];
            _cards.RemoveAt(Count - 1);
            return lastCard;
        }
        //TODO: needed?
        void IDeck.Print()
        {
        }

        void IDeck.ServePlayer(IPlayer player)
        {
            player.CurrentCards = _cards.GetRange(Count - _cardsPerPlayerCount, _cardsPerPlayerCount);
            player.CurrentCards.ForEach(x => _log.LogInformation("Rank {Rank}, Suit {Suit}", x.Rank, x.Suit));
            _cards.RemoveRange(Count - _cardsPerPlayerCount, _cardsPerPlayerCount);
        }
    }
}
