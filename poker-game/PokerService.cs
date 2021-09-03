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
            _player.Name = "Player";
            _computer = computer;
            _computer.Name = "Computer";
        }

        internal void Run()
        {
            _log.LogInformation(_deck.IsEmpty() ? "Deck is Empty." : "Deck is not empty.");
            _deck.Initialize();
            _deck.Suffle();
            _log.LogInformation("The deck has {count} cards. ", _deck.Count);

            _deck.Pop();
            _deck.Pop();
            CurrentRoundResults playerScore, computerScore;// = new CurrentRoundResults(), computerScore = new CurrentRoundResults();
            var numberOfCardsPerRound = _config.GetSection("PokerRules:#CardsToServeEachPlayer").Get<int>() * 2;//=10

            do
            {
                _log.LogInformation("------------------");
                _deck.ServePlayer(_player);
                _deck.ServePlayer(_computer);

                //TODO: create players via factory 
                playerScore = _player.EvaluateCards();
                _log.LogInformation("{name} score: [category={p}, highest rank={r}, second highest rank={s}]", _player.Name, playerScore._category, playerScore._highestRank, playerScore._2ndHighestRank);
                computerScore = _computer.EvaluateCards();

                _log.LogInformation("{name} score [category={p}, highest rank={r},  second highest rank={s}]", _computer.Name, computerScore._category, computerScore._highestRank, computerScore._2ndHighestRank);

                //report the winner  
                if (playerScore._category > computerScore._category)
                    playerScore._total++;
                else if (playerScore._category < computerScore._category)
                    computerScore._total++;
                else //equal
                {
                    if (playerScore._highestRank > computerScore._highestRank)
                        playerScore._total++;
                    else if (playerScore._highestRank < computerScore._highestRank)
                        computerScore._total++;
                    else
                    {
                        if (playerScore._2ndHighestRank > computerScore._2ndHighestRank)
                            playerScore._total++;
                        else if (playerScore._2ndHighestRank < computerScore._2ndHighestRank)
                            computerScore._total++;
                    }
                }
                _log.LogInformation("Press 'Y' to lay another round");
            } while ((Console.ReadKey(true).Key == ConsoleKey.Y) && (!_deck.IsEmpty()) && (_deck.Count >= numberOfCardsPerRound));

            _log.LogInformation("Player score: {score}", playerScore._total);
            _log.LogInformation("Computer score: {score}", computerScore._total);

            if (playerScore._total > computerScore._total)
                _log.LogInformation("Player won with a total score of {total}!", playerScore._total);
            else if (playerScore._total < computerScore._total)
                _log.LogInformation("Computer won with a total score of {total}!", computerScore._total);
            else
                _log.LogInformation("Computer and player are equal with a score of {score}!", computerScore._total);
        }
    }
}
