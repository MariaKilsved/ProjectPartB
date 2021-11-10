using System;

namespace ProjectPartB_B2
{
    class Program
    {
        static void Main(string[] args)
        {
            DeckOfCards myDeck = new DeckOfCards();
            myDeck.CreateFreshDeck();
            Console.WriteLine($"\nA freshly created deck with {myDeck.Count} cards:");
            Console.WriteLine(myDeck);

            
            Console.WriteLine($"\nA sorted deck with {myDeck.Count} cards:");
            myDeck.Sort();
            Console.WriteLine(myDeck);
            

            
            Console.WriteLine($"\nA shuffled deck with {myDeck.Count} cards:");
            myDeck.Shuffle();
            Console.WriteLine(myDeck);
            

            PokerHand Player = new PokerHand();



            //========TESTING========
            //Full House 
            /*
            Player.Add(new PlayingCard { Color = PlayingCardColor.Hearts, Value = PlayingCardValue.Ace });
            Player.Add(new PlayingCard { Color = PlayingCardColor.Clubs, Value = PlayingCardValue.Ace });
            Player.Add(new PlayingCard { Color = PlayingCardColor.Diamonds, Value = PlayingCardValue.Ace });
            Player.Add(new PlayingCard { Color = PlayingCardColor.Spades, Value = PlayingCardValue.Three });
            Player.Add(new PlayingCard { Color = PlayingCardColor.Hearts, Value = PlayingCardValue.Three });
            */

            //Straight
            /*
            Player.Add(new PlayingCard { Color = PlayingCardColor.Hearts, Value = PlayingCardValue.Ten });
            Player.Add(new PlayingCard { Color = PlayingCardColor.Spades, Value = PlayingCardValue.Nine });
            Player.Add(new PlayingCard { Color = PlayingCardColor.Diamonds, Value = PlayingCardValue.Eight });
            Player.Add(new PlayingCard { Color = PlayingCardColor.Clubs, Value = PlayingCardValue.Seven });
            Player.Add(new PlayingCard { Color = PlayingCardColor.Hearts, Value = PlayingCardValue.Six });
            */


            Console.WriteLine();
            //Print the PokerHand to the console
            Console.WriteLine($"{nameof(Player)} hand: {Player}");
            Console.WriteLine($"Rank is {Player.Rank.ToString()} with rank-high-card: {Player.RankHiCard.ToString()}");
            //Only print these out if the rank is Two Pair.
            if (Player.Rank == PokerRank.TwoPair)
            {
                Console.WriteLine($"First pair rank-high-card {Player?.RankHiCardPair1.ToString()}");
                Console.WriteLine($"Second pair rank-high-card {Player?.RankHiCardPair2.ToString()}");
            }
            //The spelling here is the same as in ProjectPartB Explanation. 
            Console.WriteLine($"Deck has now {myDeck.Count} cards");

            //========END TESTING========







            while (myDeck.Count > 5)
            {
                //Your code to Give 5 cards to the player and determine the rank
                //Continue for as long as the deck has at least 5 cards

                //First reset the PokerHand
                Player.Clear();

                try
                {
                    //Add the top 5 cards

                    for(int i = 0; i < 5; i++)
                    {
                        //Can add directly from myDeck to HandOfCards since the parameter of Add is a card just like the return value of RemoveTopCard
                        Player.Add(myDeck.RemoveTopCard());
                    }


                    Console.WriteLine();
                    //Print the PokerHand to the console
                    Console.WriteLine($"{nameof(Player)} hand: {Player}");
                    Console.WriteLine($"Rank is {Player.Rank.ToString()} with rank-high-card: {Player.RankHiCard.ToString()}");
                    //Only print these out if the rank is Two Pair.
                    if(Player.Rank == PokerRank.TwoPair)
                    {
                        Console.WriteLine($"First pair rank-high-card {Player?.RankHiCardPair1.ToString()}");
                        Console.WriteLine($"Second pair rank-high-card {Player?.RankHiCardPair2.ToString()}");
                    }
                    //The spelling here is the same as in ProjectPartB Explanation. 
                    Console.WriteLine($"Deck has now {myDeck.Count} cards");
                }
                //Catch which should never be reached. Using it for test purposes.
                catch
                {
                    Console.WriteLine("Not enough cards in deck.");
                }

            }
        }
    }
 }
