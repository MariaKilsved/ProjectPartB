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
            //Somebody in the class used StringBuilder, which led to me looking it up.
            //So now I'm using StringBuilder when I would have added a lot of strings together.
            //I initially wanted to set capacity to 10 because "\u2660 Diamonds" is 10 chars long.
            //However, that doesn't account for the appended strings being different in length so I refrained.
            StringBuilder sb = new StringBuilder();

            for(int i = 0; i < cards.Count; i++)
            {
                //It's impossible to get the spacing right in the console.
                // It looks less weird with more spacing, but then it will look terrible if your console window is too small.
                sb.Append($"{cards[i],-10}");

                if((i + 1) % 13 == 0)
                {
                    sb.Append("\n");
                }
            }
            return sb.ToString();
        }
        #endregion

        #region Shuffle and Sorting

        /// <summary>
        /// Shuffles the deck of cards
        /// </summary>
        public void Shuffle()
        {
            // I can't get enough of .OrderBy from System.Linq. I've been using it a lot now... The code is so clean looking.
            Random random = new Random();
            cards = cards.OrderBy(x => random.Next()).ToList();
        }

        /// <summary>
        /// Sort the deck of cards using List<T> Sort()>
        /// </summary>
        public void Sort()
        {
            //Since PlayingCard implements IComparable<T>, this should work out nicely.
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
            //I was looking up the syntax of Enum.GetValues.
            //But according to this site I visited, Enum.GetNames would be better since it's 10x faster:
            //https://www.csharp411.com/c-count-items-in-an-enum/
            //In my case there's no difference between using .GetNames and .GetValues, since I'm not using any duplicate values.
            //So I decided to use .GetNames.
            for (int i = 0; i < Enum.GetNames(typeof(PlayingCardColor)).Length; i++)
            {
                for(int j = 2; j < Enum.GetNames(typeof(PlayingCardValue)).Length + 2; j++)
                {
                    cards.Add(new PlayingCard { Color = (PlayingCardColor)i, Value = (PlayingCardValue)j });
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
            cards.RemoveAt(0);
            return card;
        }
        #endregion
    }
}
