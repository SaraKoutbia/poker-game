using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Text;
using System;

namespace poker_game.tests
{
    public class DeckTests
    {
        [Fact]
        public void Initialize_PopulatesTheDeckWith52Cards()
        {
            //setup - create the IConfiguration object then a mock deck object 
            var appSettings = @"{
                                  ""PokerRules"": {
                                                      ""#CardsPerSuit"": 13,
                                                      ""#CardsToServeEachPlayer"": 5
                                                   }            
                               }";

            var builder = new ConfigurationBuilder();
            builder.AddJsonStream(new MemoryStream(Encoding.UTF8.GetBytes(appSettings)));
            var configuration = builder.Build();
            var deck = new Mock<Deck>(new Mock<ILogger<PokerService>>().Object, configuration).Object;

            //exercise  
            deck.Initialize();
            var numberOfCards = configuration.GetSection("PokerRules:#CardsPerSuit").Get<int>() *
                                    Enum.GetNames(typeof(Enums.Suit)).Length;

            //verify
            Assert.Equal(numberOfCards, deck.Count);
        }

        [Fact]
        public void IsEmpty_FullDeckReturnsFalse()
        {

            //setup - create the IConfiguration object then a mock deck object 
            var appSettings = @"{
                                  ""PokerRules"": {
                                  ""#CardsPerSuit"": 13,
                                  ""#CardsToServeEachPlayer"": 5
                                 }            
                               }";

            var builder = new ConfigurationBuilder();
            builder.AddJsonStream(new MemoryStream(Encoding.UTF8.GetBytes(appSettings)));
            var configuration = builder.Build();
            var deck = new Mock<Deck>(new Mock<ILogger<PokerService>>().Object, configuration).Object;
            deck.Initialize();

            //exercise  and verify
            Assert.False(deck.IsEmpty());
        }

        [Fact]
        public void IsEmpty_EmptyDeckReturnsTrue()
        {

            //setup - create the IConfiguration object then a mock deck object 
            var appSettings = @"{
                                  ""PokerRules"": {
                                  ""#CardsPerSuit"": 13,
                                  ""#CardsToServeEachPlayer"": 5
                                 }            
                               }";

            var builder = new ConfigurationBuilder();
            builder.AddJsonStream(new MemoryStream(Encoding.UTF8.GetBytes(appSettings)));
            var configuration = builder.Build();
            var deck = new Mock<Deck>(new Mock<ILogger<PokerService>>().Object, configuration).Object;

            //exercise  and verify
            Assert.True(deck.IsEmpty());
        }

        [Fact]
        public void ServePlayer_DecksLastCardsEqPlayersCards()
        {
            //setup - create the IConfiguration object then a mock deck object 
            var appSettings = @"{
                                  ""PokerRules"": {
                                  ""#CardsPerSuit"": 13,
                                  ""#CardsToServeEachPlayer"": 5
                                 }            
                               }";

            var builder = new ConfigurationBuilder();
            builder.AddJsonStream(new MemoryStream(Encoding.UTF8.GetBytes(appSettings)));
            var configuration = builder.Build();

            var deck = new Mock<Deck>(new Mock<ILogger<PokerService>>().Object, configuration).Object;
            deck.Initialize();
            var player = new Mock<Player>(new Mock<ILogger<Player>>().Object, configuration).Object;
            var cardsPerPlayer = configuration.GetSection("PokerRules:#CardsToServeEachPlayer").Get<int>();

            //exercise
            var deck5LastCards = deck._cards.GetRange(deck._cards.Count - cardsPerPlayer, cardsPerPlayer);
            deck.ServePlayer(player);

            //and verify
            Assert.Equal(deck5LastCards, player.CurrentCards);
        }

        [Fact]
        public void Pop_ReturnsLastItemOfTheDeck()
        {
            //setup - create the IConfiguration object then a mock deck object 
            var appSettings = @"{
                                  ""PokerRules"": {
                                  ""#CardsPerSuit"": 13,
                                  ""#CardsToServeEachPlayer"": 5
                                 }            
                               }";

            var builder = new ConfigurationBuilder();
            builder.AddJsonStream(new MemoryStream(Encoding.UTF8.GetBytes(appSettings)));
            var configuration = builder.Build();

            var deck = new Mock<Deck>(new Mock<ILogger<PokerService>>().Object, configuration).Object;
            deck.Initialize();

            //exercise 
            var lastCardInDeck = deck._cards[deck._cards.Count - 1];
            var card = deck.Pop();

            //verify 
            Assert.Equal(lastCardInDeck, card);
        }

    }
}
