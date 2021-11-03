using System;

namespace ProjectPartB_B1
{
    class Program
    {
        static void Main(string[] args)
        {
            DeckOfCards myDeck = new DeckOfCards();
            myDeck.CreateFreshDeck();
            Console.WriteLine(myDeck.Count);

            Console.WriteLine($"\nA freshly created deck with {myDeck.Count} cards:");
            Console.WriteLine(myDeck);
            
            Console.WriteLine($"\nA sorted deck with {myDeck.Count} cards:");
            myDeck.Sort();
            Console.WriteLine(myDeck);

            Console.WriteLine($"\nA shuffled deck with {myDeck.Count} cards:");
            myDeck.Shuffle();
            Console.WriteLine(myDeck);

            HandOfCards player1 = new HandOfCards();
            HandOfCards player2 = new HandOfCards();

            //Your code to play the game comes here
            Console.WriteLine("Let's play a game of highest card with two players.");

            //"How many cards to deal to each player (1-5 or Q to quit)?"
            int NrOfCards = 0;
            bool Continue = TryReadNrOfCards(out NrOfCards);
            if (!Continue) return;

            //"How many rounds should we play (1-5 or Q to quit)?"
            int NrOfRounds = 0;
            Continue = TryReadNrOfRounds(out NrOfRounds);
            if (!Continue) return;

            //I think it would have been nice to encapsulate this
            for(int i = 0; i < NrOfRounds; i++)
            {
                Console.WriteLine($"Playing round nr {i + 1}");
                Console.WriteLine("------------------");
                Console.WriteLine("Gave ");
            }




        }

        /// <summary>
        /// Asking a user to give how many cards should be given to players.
        /// User enters an integer value between 1 and 5. 
        /// </summary>
        /// <param name="NrOfCards">Number of cards given by user</param>
        /// <returns>true - if value could be read and converted. Otherwise false</returns>
        private static bool TryReadNrOfCards(out int NrOfCards)
        {
            NrOfCards = 0;
            string sInput;

            //This is nearly identical to GroupAssignment3
            //Should be merged with TryReadNrOfRounds, but I won't do it since you wrote it this way...
            do
            {
                Console.WriteLine("How many cards to deal to each player (1-5 or Q to quit)?");
                sInput = Console.ReadLine();
                if (int.TryParse(sInput, out NrOfCards) && NrOfCards >= 1 && NrOfCards <= 5)
                {
                    return true;
                }
                else if (sInput != "Q" && sInput != "q")
                {
                    Console.WriteLine("Wrong input, please try again.");
                }
            }
            while ((sInput != "Q" && sInput != "q"));
            return false;
        }

        /// <summary>
        /// Asking a user to give how many round should be played.
        /// User enters an integer value between 1 and 5. 
        /// </summary>
        /// <param name="NrOfRounds">Number of rounds given by user</param>
        /// <returns>true - if value could be read and converted. Otherwise false</returns>
        private static bool TryReadNrOfRounds(out int NrOfRounds)
        {
            //In GroupAssignment3, we used a more general TryReadInt32 instead of two methods doing nearly the same thing...
            //Why go back to multiple methods instead of just one?
            //Oh well, I'll stick with two methods since you made it that way
            NrOfRounds = 0;
            string sInput;
            do
            {
                Console.WriteLine("How many rounds should we play (1-5 or Q to quit)?");
                sInput = Console.ReadLine();
                if (int.TryParse(sInput, out NrOfRounds) && NrOfRounds >= 1 && NrOfRounds <= 5)
                {
                    return true;
                }
                else if (sInput != "Q" && sInput != "q")
                {
                    Console.WriteLine("Wrong input, please try again.");
                }
            }
            while ((sInput != "Q" && sInput != "q"));
            return false;
        }


        /// <summary>
        /// Removes from myDeck one card at the time and gives it to player1 and player2. 
        /// Repeated until players have recieved nrCardsToPlayer 
        /// </summary>
        /// <param name="myDeck">Deck to remove cards from</param>
        /// <param name="nrCardsToPlayer">Number of cards each player should recieve</param>
        /// <param name="player1">Player 1</param>
        /// <param name="player2">Player 2</param>
        private static void Deal(DeckOfCards myDeck, int nrCardsToPlayer, HandOfCards player1, HandOfCards player2)
        {
            //Looping a number of times corresponding to the number of cards that should be received

            for(int i = 0; i < nrCardsToPlayer; i++)
            {
                //Can add directly from myDeck to HandOfCards since the parameter of Add is a card just like the return value of RemoveTopCard
                //However, myDeck might be out of cards which necessiates error handling.
                try 
                {
                    player1.Add(myDeck.RemoveTopCard());
                    player2.Add(myDeck.RemoveTopCard());
                }
                catch
                {
                    //I chose to print out when there are no more cards left.
                    Console.WriteLine("Not enough cards in deck.");
                    throw;
                }
            }
        }
        
        /// <summary>
        /// Determines and writes to Console the winner of player1 and player2. 
        /// Player with higest card wins. If both cards have equal value it is a tie.
        /// </summary>
        /// <param name="player1">Player 1</param>
        /// <param name="player2">Player 2</param>
        private static void DetermineWinner(HandOfCards player1, HandOfCards player2)
        {
            //Thanks to implementing IComparable<T> on PlayingCard, this works:
            int cmpVal = player1.Highest.CompareTo(player2.Highest);

            if(cmpVal < 0)
            {
                Console.WriteLine("Player1 wins!");
            }
            else if(cmpVal > 0)
            {
                Console.WriteLine("Player2 wins!");
            }
            else
            {
                Console.WriteLine("It is a tie.");
            }

        }
    }
}
