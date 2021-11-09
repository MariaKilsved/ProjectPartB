using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPartB_B1
{
    class DeckOfCards : IDeckOfCards
    {
        #region cards List related
        protected const int MaxNrOfCards = 52;
        protected List<PlayingCard> cards = new List<PlayingCard>(MaxNrOfCards);

        /// <summary>
        /// The card at a particular position in the deck.
        /// </summary>
        /// <param name="idx">0 based position in the deck</param>
        /// <returns>The card at [idx] position</returns>
        public PlayingCard this[int idx] 
        { 
            get
            {
                //Basic indexer
                if (idx < 0 || idx >= cards.Count)
                    throw new IndexOutOfRangeException("Index out of range");
                return cards[idx];
            }
        }

        /// <summary>
        /// Number of Cards in the deck
        /// </summary>
        public int Count => cards.Count;
        #endregion

        #region ToString() related
        //Should be overriden and implemented to print out the complete deck in short card notation
        public override string ToString()
        {
            //I was using StringBuilder here intially
            string sRet = "";

            for (int i = 0; i < cards.Count; i++)
            {
                //It's impossible to get the spacing right in the console.
                // It looks less weird with more spacing, but then it will look terrible if your console window is too small.
                sRet += $"{cards[i],-10}";

                if ((i + 1) % 13 == 0)
                {
                    sRet += "\n";
                }
            }
            return sRet;

        }
        #endregion

        #region Shuffle and Sorting

        /// <summary>
        /// Shuffles the deck of cards
        /// </summary>
        public void Shuffle()
        {
            //I can't get enough of .OrderBy from System.Linq. I've been using it a lot now... The code is so clean looking.
            //So, what do I know about OrderBy?
            //First of all, I don't know a lot about LINQ yet, but I know it's about queries to databases or datasets.
            //As for OrderBy itself, I know the basic use and that it uses deferred execution.
            //It uses a key or comparer to sort elements, which will be random in the case of using random.Next().
            //If you don't add ToList at the end to convert the result into a list, or convert it into something else, it will be an IOrderedEnumerable.
            //I don't know about all the uses for IOrderedEnumerable; there seems to be a lot.
            Random random = new Random();
            cards = cards.OrderBy(x => random.Next()).ToList();
        }

        /// <summary>
        /// Sort the deck of cards using List<T> Sort()>
        /// </summary>
        public void Sort()
        {
            //Since PlayingCard implements IComparable<T>, simply using Sort should work out nicely.
            cards.Sort();
        }
        #endregion

        #region Creating a fresh Deck

        /// <summary>
        /// Empties the deck of cards so no cards in the deck
        /// </summary>
        public void Clear()
        {
            cards.Clear();
        }

        /// <summary>
        /// Initialize a fresh deck consisting of 52 cards. 
        /// Added in order Clubs (Two..Ace), Diamonds (Two..Ace), Hearts (Two..Ace), Spades (Two..Ace) 
        /// </summary>
        public void CreateFreshDeck()
        {
            //Just looping through both enums to get cards with all possible values
            //I decied to just use .GetEnumValues instead of .GetEnumNames since I want to be able to easily convert back into enums
            foreach (var col in typeof(PlayingCardColor).GetEnumValues())
            {
                foreach (var val in typeof(PlayingCardValue).GetEnumValues())
                {
                    cards.Add(new PlayingCard { Color = (PlayingCardColor)col, Value = (PlayingCardValue)val });
                }
            }
        }
        #endregion

        #region Dealing

        /// <summary>
        /// Removes the top card of the deck and reduces the number of cards in the deck with one.
        /// </summary>
        /// <returns>The removed card</returns>
        public PlayingCard RemoveTopCard()
        {
            // Creating an identical card:
            PlayingCard card = new PlayingCard { Color = cards[0].Color, Value = cards[0].Value };

            //It's unclear if the top card should be the last card or the first card in the list.
            //I'm going with the first card.
            //Using RemoveAt to remove the 0th index card:

            //By default, RemoveAt will throw an ArgumentOutOfRangeException if it doesn't work.
            //It makes sense to handle a potential error outside this method since you'd want the entire method to fail if you can't remove a card.
            cards.RemoveAt(0);

            return card;
        }
        #endregion
    }
}
