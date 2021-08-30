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
        }

    }
}
