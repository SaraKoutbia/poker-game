﻿namespace poker_game
{
    internal interface IDeck
    {
        void Initialize();
        void Suffle();
        bool IsEmpty();
        Card Pop();
        public int Count { get; }
        void Print();
        void ServePlayer(IPlayer player);
    }
}