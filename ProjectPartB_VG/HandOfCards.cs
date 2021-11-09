using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPartB_B2
{
    class HandOfCards : DeckOfCards, IHandOfCards
    {
        #region Pick and Add related

        /// <summary>
        /// Add a card to the hand and sorts the hand
        /// </summary>
        /// <param name="card">Card to be added</param>
        public virtual void Add(PlayingCard card)
        {
            cards.Add(card);
            cards.Sort();
        }
        #endregion

        #region Highest Card related

        /// <summary>
        /// The Higest card in the sorted hand
        /// </summary>
        public PlayingCard Highest
        {
            get
            {
                //Unnecessary to sort since it's sorted in Add and will always be sorted. 
                return cards[^1];
            }
        }

        /// <summary>
        /// The Lowest card in the sorted hand
        /// </summary>
        public PlayingCard Lowest
        {
            get
            {
                //Unnecessary to sort since it's sorted in Add and will always be sorted.
                return cards[0];
            }
        }
        #endregion
    }
}
