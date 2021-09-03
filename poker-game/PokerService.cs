using System;
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
            var numberOfCardsPerRound = _config.GetSection("PokerRules:#CardsToServeEachPlayer").Get<int>() * 2;//=10

            do
            {
                _log.LogInformation("------------------");
                foreach (var player in new IPlayer[] { _player, _computer })
                {
                    _deck.ServePlayer(player);
                    player.EvaluateCards();
                    player.PrintCurrentScore();
                }
                ComputeEachPlayersTotal();
                _log.LogInformation("Press 'Y' to play another round");

            } while ((Console.ReadKey(true).Key == ConsoleKey.Y) && (!_deck.IsEmpty()) && (_deck.Count >= numberOfCardsPerRound));

            PrintWinner();
        }
        private void PrintWinner()
        {
            var playerTotal = _player.Score._total;
            var computerTotal = _computer.Score._total;

            _player.PrintTotal();
            _computer.PrintTotal();

            if (playerTotal > computerTotal)
                _log.LogInformation("{p} won with a total score of {total}!", _player.Name, playerTotal);
            else if (playerTotal < computerTotal)
                _log.LogInformation("{p} won with a total score of {total}!", _computer.Name, computerTotal);
            else
                _log.LogInformation("{c} and {p} are equal with a score of {score}!", _computer.Name, _player.Name, computerTotal);
        }

        private void ComputeEachPlayersTotal()
        {
            CurrentRoundResults playerScore, computerScore;
            playerScore = _player.Score._currentRoundResults;
            computerScore = _computer.Score._currentRoundResults;

            //report the winner  
            if (playerScore._category > computerScore._category)
                _player.Score._total++;
            else if (playerScore._category < computerScore._category)
                _computer.Score._total++;
            else //equal
            {
                if (playerScore._highestRank > computerScore._highestRank)
                    _player.Score._total++;
                else if (playerScore._highestRank < computerScore._highestRank)
                    _computer.Score._total++;
                else
                {
                    if (playerScore._2ndHighestRank > computerScore._2ndHighestRank)
                        _player.Score._total++;
                    else if (playerScore._2ndHighestRank < computerScore._2ndHighestRank)
                        _computer.Score._total++;
                }
            }
        }
    }
}
