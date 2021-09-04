using System.Collections;
using System.Collections.Generic;
using Xunit;

using static poker_game.Enums;

namespace poker_game.tests
{

    internal class ScoreTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] {//StraightFlush = 1 - Five cards of the same suit in sequence (if those five are A, K, Q, J, 10; it is a Royal Flush)
                  new List<Card>(){
                                    new Card(){Suit=Suit.Clubs, Rank=4 },
                                    new Card(){Suit=Suit.Clubs, Rank=5 },
                                    new Card(){Suit=Suit.Clubs, Rank=6 },
                                    new Card(){Suit=Suit.Clubs, Rank=7 },
                                    new Card(){Suit=Suit.Clubs, Rank=8 }
                  },
                new CurrentRoundResults(){_category= Category.StraightFlush, _highestRank= 8, _2ndHighestRank=7}
            };


            yield return new object[] {//FourOfAKind = 2 - Four cards of the same rank and any one other card 
               new List<Card>(){
                                    new Card(){Suit=Suit.Clubs, Rank=7 },
                                    new Card(){Suit=Suit.Diamonds, Rank=7 },
                                    new Card(){Suit=Suit.Clubs, Rank=3 },
                                    new Card(){Suit=Suit.Clubs, Rank=7 },
                                    new Card(){Suit=Suit.Spades, Rank=7 }
                },
                new CurrentRoundResults(){_category= Category.FourOfAKind, _highestRank= 7, _2ndHighestRank=3}
                };


            yield return new object[] {//FullHouse = 3 - Three cards of one rank and two of another 
               new List<Card>(){
                                    new Card(){Suit=Suit.Clubs, Rank=12 },
                                    new Card(){Suit=Suit.Diamonds, Rank=12 },
                                    new Card(){Suit=Suit.Clubs, Rank=13 },
                                    new Card(){Suit=Suit.Clubs, Rank=12 },
                                    new Card(){Suit=Suit.Spades, Rank=13 }
                },
                new CurrentRoundResults(){_category= Category.FullHouse, _highestRank= 12, _2ndHighestRank=13}
                };


            yield return new object[] {//Flush = 4 -Five cards of the same suit
               new List<Card>(){
                                    new Card(){Suit=Suit.Clubs, Rank=6 },
                                    new Card(){Suit=Suit.Clubs, Rank=2 },
                                    new Card(){Suit=Suit.Clubs, Rank=9 },
                                    new Card(){Suit=Suit.Clubs, Rank=12 },
                                    new Card(){Suit=Suit.Clubs, Rank=10 }
                },
                new CurrentRoundResults(){_category= Category.Flush, _highestRank= 12, _2ndHighestRank=10}
                };

            yield return new object[] {//Straight = 5 - Five cards in sequence (for example, 4, 5, 6, 7, 8) 
               new List<Card>(){
                                    new Card(){Suit=Suit.Clubs, Rank=6 },
                                    new Card(){Suit=Suit.Clubs, Rank=7 },
                                    new Card(){Suit=Suit.Spades, Rank=9 },
                                    new Card(){Suit=Suit.Clubs, Rank=8 },
                                    new Card(){Suit=Suit.Diamonds, Rank=10 }
                },
                new CurrentRoundResults(){_category= Category.Straight, _highestRank= 10, _2ndHighestRank=9}
                };

            yield return new object[] {//ThreeOfAKind = 6 - Three cards of the same rank
               new List<Card>(){
                                    new Card(){Suit=Suit.Clubs, Rank=8 },
                                    new Card(){Suit=Suit.Clubs, Rank=8 },
                                    new Card(){Suit=Suit.Spades, Rank=9 },
                                    new Card(){Suit=Suit.Clubs, Rank=8 },
                                    new Card(){Suit=Suit.Diamonds, Rank=10 }
                },
                new CurrentRoundResults(){_category= Category.ThreeOfAKind, _highestRank= 8, _2ndHighestRank=10}
                };

            yield return new object[] {//TwoPair = 7 - Two cards of one rank and two cards of another
               new List<Card>(){
                                    new Card(){Suit=Suit.Clubs, Rank=8 },
                                    new Card(){Suit=Suit.Clubs, Rank=8 },
                                    new Card(){Suit=Suit.Spades, Rank=9 },
                                    new Card(){Suit=Suit.Clubs, Rank=10 },
                                    new Card(){Suit=Suit.Diamonds, Rank=10 }
                },
                new CurrentRoundResults(){_category= Category.TwoPair, _highestRank= 10, _2ndHighestRank=8}
                };

            yield return new object[] {//OnePair = 8 - Two cards of the same rank 
                 new List<Card>(){
                                    new Card(){Suit=Suit.Clubs, Rank=8 },
                                    new Card(){Suit=Suit.Clubs, Rank=8 },
                                    new Card(){Suit=Suit.Spades, Rank=9 },
                                    new Card(){Suit=Suit.Clubs, Rank=11 },
                                    new Card(){Suit=Suit.Diamonds, Rank=10 }
                },
                new CurrentRoundResults(){_category= Category.OnePair, _highestRank= 8, _2ndHighestRank=11}
                };

            yield return new object[] {//HighCard = 9 - If no one has a pair, the highest card wins 
                 new List<Card>(){
                                    new Card(){Suit=Suit.Clubs, Rank=8 },
                                    new Card(){Suit=Suit.Clubs, Rank=2 },
                                    new Card(){Suit=Suit.Spades, Rank=7 },
                                    new Card(){Suit=Suit.Clubs, Rank=4 },
                                    new Card(){Suit=Suit.Diamonds, Rank=3 }
                },
                new CurrentRoundResults(){_category= Category.HighCard, _highestRank= 8, _2ndHighestRank=7}
                };
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }


    public class EvaluatorTests
    {
        [Theory]
        [ClassData(typeof(ScoreTestData))]
        public void GetCurrentScore_Tests(List<Card> cards, CurrentRoundResults roundScore)
        {
            //exercise 
            var roundResults = Evaluator.GetCurrentScore(cards);

            //verify
            Assert.Equal(roundScore._category, roundResults._category);
            Assert.Equal(roundScore._highestRank, roundResults._highestRank);
            Assert.Equal(roundScore._2ndHighestRank, roundResults._2ndHighestRank);
        }

    }
}
