using System;
using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using poker_game;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Text;

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

            //verify
            Assert.Equal(52, deck.Count);
        }


    }
}
