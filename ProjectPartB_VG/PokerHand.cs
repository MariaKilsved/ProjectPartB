using System;

namespace ProjectPartB_B2
{
    class PokerHand : HandOfCards
    {
        //public enum PokerRank { Unknown, HighCard, Pair, TwoPair, ThreeOfAKind, Straight, Flush, FullHouse, FourOfAKind, StraightFlush, RoyalFlush }

        #region Clear
        //Override Clear in DeckOfCards to also clear rank
        public override void Clear()
        {
            _rank = PokerRank.Unknown;
            cards.Clear();
        }
        #endregion

        #region Remove and Add related
        //Override Add in HandOfCards to also clear rank
        public override void Add(PlayingCard card)
        {
            _rank = PokerRank.Unknown;
            cards.Add(card);
            cards.Sort();
        }
        #endregion

        #region Poker Rank related
        //https://www.poker.org/poker-hands-ranking-chart/

        //Hint: using backing fields
        private PokerRank _rank = PokerRank.Unknown;
        private PlayingCard _rankHigh = null;
        private PlayingCard _rankHighPair1 = null;
        private PlayingCard _rankHighPair2 = null;

        /// <summary>
        /// The rank of the pokerhand determined using DetermineRank().
        /// </summary>
        public PokerRank Rank => _rank;

        /// <summary>
        /// The highest card in a rank when rank. 
        /// Null when rank is PokerRank.Undefined
        /// </summary>
        public PlayingCard RankHiCard => (Rank == PokerRank.Unknown) ? null : _rankHigh;

        /// <summary>
        /// The highest card in the first pair when rank is PokerRank.TwoPair, otherwise null
        /// </summary>
        public PlayingCard RankHiCardPair1 => _rankHighPair1;

        /// <summary>
        /// The highest card in the second pair when rank is PokerRank.TwoPair, otherwise null
        /// </summary>
        public PlayingCard RankHiCardPair2 => _rankHighPair2;

        //Hint: Worker Methods to examine a sorted hand
        private int NrSameValue(int firstValueIdx, out int lastValueIdx, out PlayingCard HighCard) 
        {
            lastValueIdx = firstValueIdx;
            HighCard = cards[4];

            int counter = 0;
            // First, a loop through all cards except the first one
            for (int i = firstValueIdx + 1; i < cards.Count; i++)
            {
                //Check if cards have the same value as the first one. Increase the values by 1 if they do.
                if (cards[firstValueIdx].Value == cards[i].Value)
                {
                    lastValueIdx++;
                    counter++;
                }
            }
            return counter; 
        }
        private bool IsSameColor(out PlayingCard HighCard)
        {
            HighCard = cards[4];
            //If any of the cards have a different color, return false
            //Essentially tests if it is a Flush
            for (int i = 1; i < cards.Count; i++)
            {
                if (cards[0].Color != cards[i].Color)
                {
                    return false;
                }
            }
            return true;
        }
        private bool IsConsecutive(out PlayingCard HighCard)
        {
            HighCard = cards[4];
            //If any two consecutive cards doesn't have a difference of 1 in value, return false
            //Essentially tests for a Straight
            for (int i = 0; i < cards.Count - 1; i++)
            {
                if (cards[i].Value - 1 != cards[i + 1].Value)
                {
                    return false;
                }
            }
            return true;
        }

        //Hint: Worker Properties to examine each rank
        private bool IsRoyalFlush 
        {
            get
            {
                PlayingCard HighCard = null;
                if (IsSameColor(out HighCard) && IsConsecutive(out HighCard))
                {
                    if (HighCard.Value == PlayingCardValue.Ace)
                        return true;
                }
                return false;
            }
        }
        private bool IsStraightFlush
        {
            get
            {
                if (IsSameColor(out _) && IsConsecutive(out _))
                    return true;
                return false;
            }
        }
        private bool IsFourOfAKind
        {
            get
            {
                if (NrSameValue(0, out _, out _) == 4 || NrSameValue(1, out _, out _) == 4)
                {
                    return true;
                }
                return false;
            }
        }
        private bool IsFullHouse 
        {
            get
            {
                //If the first two cards aren't the same, or if the final two cards aren't the same, return false
                if (cards[0].Value != cards[1].Value || cards[3].Value != cards[4].Value)
                    return false;

                //If the middle card isn't the same as the 2nd card, and the middle card isn't the same as the 4th card, return false
                if (cards[2].Value != cards[1].Value && cards[2].Value != cards[3].Value)
                    return false;

                return true;

            }
        }
        private bool IsFlush
        {
            get
            {
                return IsSameColor(out _);
            }
        }
        private bool IsStraight
        {
            get
            {
                return IsConsecutive(out _);
            }
        }
        private bool IsThreeOfAKind
        {
            get
            {
                if (NrSameValue(0, out _, out _) == 3 || NrSameValue(1, out _, out _) == 3 || NrSameValue(2, out _, out _) == 3)
                    return true;
                return false;

                /*
                //Alternative to the above:
                if(cards[0].Value == cards[2].Value || cards[1].Value == cards[3].Value || cards[2].Value == cards[4].Value)
                    return true;
                return false;
                */
            }
        }
        private bool IsTwoPair
        {
            get
            {
                //private int NrSameValue(int firstValueIdx, out int lastValueIdx, out PlayingCard HighCard)
                int lastValueIdx = 0;
                PlayingCard HighCard = null;
                int duplicates = NrSameValue(0, out lastValueIdx, out HighCard);



                return false;

                /*
                int nrOfPairs = 0;

                //Loop through the cards and count any duplicates in value. 
                //Will regard three and four of a kind as pairs, but that doesn't matter as those are higher ranks
                for(int i = 0; i < cards.Count - 1; i++)
                {
                    if (cards[i].Value == cards[i + 1].Value)
                        nrOfPairs++;
                }
                //Returns true if there are at least two duplicates
                if (nrOfPairs >= 2)
                    return true;

                return false;
                */
            }
        }
        private bool IsPair
        {
            get
            {
                //Loop through the cards and returns true if any duplicates in value are found
                for (int i = 0; i < cards.Count - 1; i++)
                {
                    if (cards[i].Value == cards[i + 1].Value)
                        return true;
                }
                return false;
            }
        }


        /// <summary>
        /// Got through a 5 card hand and determine the Rank according to
        /// https://www.poker.org/poker-hands-ranking-chart/
        /// After determination properties Rank and RankHiCard can be read.
        /// In case Rank is PokerRank.TwoPair, RankHiCardPair1 and RankHiCardPair2 
        /// are set and RankHiCard is set to higest of RankHiCardPair1 and RankHiCardPair2
        /// </summary>
        /// <returns>The pokerhand rank. PokerRank.Undefined in case Hand is not 5 cards</returns>
        public PokerRank DetermineRank()
        {
            //Default to HighCard
            PokerRank theRank = PokerRank.HighCard;

            //Check for poker hand ranks from lowest to highest
            if (IsPair)
                theRank = PokerRank.Pair;
            if (IsTwoPair)
                theRank = PokerRank.TwoPair;
            if (IsThreeOfAKind)
                theRank = PokerRank.ThreeOfAKind;
            if (IsStraight)
                theRank = PokerRank.Straight;
            if (IsFlush)
                theRank = PokerRank.Flush;
            if (IsFullHouse)
                theRank = PokerRank.FullHouse;
            if (IsFourOfAKind)
                theRank = PokerRank.FourOfAKind;
            if (IsStraightFlush)
                theRank = PokerRank.StraightFlush;
            if (IsRoyalFlush)
                theRank = PokerRank.RoyalFlush;


            /*
                private PokerRank _rank = PokerRank.Unknown;
                private PlayingCard _rankHigh = null;
                private PlayingCard _rankHighPair1 = null;
                private PlayingCard _rankHighPair2 = null;
             */

            return theRank;


        }

        //Hint: Clear rank
        private void ClearRank()
        {
            _rankHigh = null;
            _rankHighPair1 = null;
            _rankHighPair2 = null;
            _rank = PokerRank.Unknown;
        }
        #endregion
    }
}
