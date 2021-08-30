using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;


namespace poker_game
{
    internal class PokerService : IPokerService
    {
        private readonly ILogger<PokerService> _log;
        private readonly IConfiguration _config;
        private IDeck _deck;
        private IPlayer _player, _computer;
        public PokerService(ILogger<PokerService> log, IConfiguration config, IDeck deck, IPlayer player, IPlayer computer)
        {
            _log = log;
            _config = config;
            _deck = deck;
            _player = player;
            _computer = computer;
        }
        internal void Run()
        {
            _log.LogInformation(_deck.IsEmpty() ? "Deck is Empty." : "Deck is not empty.");
            _deck.Initialize();

            _deck.Pop();
            _deck.Pop();

            var numberOfCardsPerRound = _config.GetSection("PokerRules:#CardsToServeEachPlayer").Get<int>() * 2;//=10
            while ((!_deck.IsEmpty()) && (_deck.Count >= numberOfCardsPerRound))
            {
                //deck is a stack - serve player 
                _log.LogInformation("The deck has {count} cards. ", _deck.Count);

                _deck.ServePlayer(_player);
                _deck.ServePlayer(_computer);

                //TODO: create players via factory 
                Score playerScore = _player.EvaluateCards();
                Score computerScore = _computer.EvaluateCards();

                _log.LogInformation("Player score - Category:{p} Highest rank:{r}", playerScore._currentCategory, playerScore._currentHighestRank);
                _log.LogInformation("Computer score - Category:{p} Highest rank:{r}", computerScore._currentHighestRank, computerScore._currentHighestRank);                
                
                //report the winner  
            }
        }

    }
}
