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
        public PlayingCard RankHiCard => (Rank == PokerRank.Unknown)? null : _rankHigh;

        /// <summary>
        /// The highest card in the first pair when rank is PokerRank.TwoPair, otherwise null
        /// </summary>
        public PlayingCard RankHiCardPair1 => (Rank == PokerRank.TwoPair)? _rankHighPair1 : null;

        /// <summary>
        /// The highest card in the second pair when rank is PokerRank.TwoPair, otherwise null
        /// </summary>
        public PlayingCard RankHiCardPair2 => (Rank == PokerRank.TwoPair)? _rankHighPair2 : null;

        //Hint: Worker Methods to examine a sorted hand

        /// <summary>
        /// Tests to see if any of the following cards in the PokerHand have the same value as the card with th
        /// </summary>
        /// <param name="firstValueIdx">Index of the card to be tested for duplicate values</param>
        /// <param name="lastValueIdx">Index of the last card to have the same value as the card on firstValueIdx</param>
        /// <param name="HighCard">Highest card among the duplicate cards, or the highest card in the PokerHand if there is no duplicate</param>
        /// <returns>The number of duplicate cards found.</returns>
        private int NrSameValue(int firstValueIdx, out int lastValueIdx, out PlayingCard HighCard) 
        {
            int counter = 0;
            lastValueIdx = firstValueIdx;
            HighCard = cards[4];

            if (firstValueIdx < 4 && cards[firstValueIdx].Value == cards[firstValueIdx + 1].Value)
            {
                counter++;
                counter = NrSameValue(firstValueIdx + 1, out _, out _);
                return counter;
            }
            else
            {
                lastValueIdx = firstValueIdx;
                return counter;
            }



            /*
            //===ORIGINAL SOLUTION===
            //Last index will be the same as the first if there are no duplicates
            lastValueIdx = firstValueIdx;

            //HighCard defaults to the last card if there are no identical values
            HighCard = cards[4];

            //Counter for number of duplicates found
            int counter = 0;
            // Loop through all cards following the firstValueIdx
            for (int i = firstValueIdx + 1; i < cards.Count; i++)
            {
                //Check if cards have the same value as the firstValueIdx. Increase the counter and lastValueIdx by 1 if they do.
                if (cards[firstValueIdx].Value == cards[i].Value)
                {
                    lastValueIdx++;
                    counter++;
                    //Set HighCard to the highest card found among the duplicates
                    HighCard = cards[i];
                }
            }
            return counter; 
            */
        }

        /// <summary>
        /// Tests to see if all cards have the same color
        /// </summary>
        /// <param name="HighCard">Highest card in the PokerHand</param>
        /// <returns>True or false depending on the result</returns>
        private bool IsSameColor(out PlayingCard HighCard)
        {
            //HighCard will be the last card no matter what
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

        /// <summary>
        /// Tests to see if all cards are consecutive, with only a one point difference in value between each card.
        /// </summary>
        /// <param name="HighCard">Highest card in the PokerHand</param>
        /// <returns>True or false depending on the result</returns>
        private bool IsConsecutive(out PlayingCard HighCard)
        {
            //HighCard will be the last card no matter what
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
                //I'm using HighCard here, but I could have just used cards[4].Value... It's the same thing.
                if (IsSameColor(out _) && IsConsecutive(out HighCard))
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
                //Since there are 5 cards, only the 0th and 1st index need to be tested with NrSameValue
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
                //Since there are only 5 cards, it's unnecessary to test the last two cards.
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
                bool firstPair = false;
                PlayingCard HighCard = cards[4];

                //Loop through all cards except the last one to check for duplicates
                for(int i = 0; i < cards.Count - 1; i++)
                {
                    //How many duplicates in value does cards[i] have?
                    int duplicates = NrSameValue(i, out lastValueIdx, out HighCard);

                    //If another pair was found before, and there is a second pair now, return true
                    if (duplicates == 1 && firstPair)
                    {
                        return true;
                    }

                    //There is at least one pair if the number of duplicates is exactly 1
                    if (duplicates == 1 && !firstPair) 
                    {
                        firstPair = true;

                        //Skip ahead to the lastValueIdx.
                        //Though confusing, I want to use lastValueIdx for something. Or I might just discard it.
                        i = lastValueIdx;   
                    }
                }
                return false;
            }
        }
        private bool IsPair
        {
            get
            {
                PlayingCard HighCard = cards[4];

                for (int i = 0; i < cards.Count - 1; i++)
                {
                    //Specifically testing for pairs and not for triple cards
                    //Will return true as soon as 1 pair is found, ignoring any potential card combinations on higher indexes in the PokerHand.
                    if(NrSameValue(i, out _, out HighCard) == 1)
                    {
                        return true;
                    }
                }
                return false;

                /*
                //Alternative to the above:
                for (int i = 0; i < cards.Count - 1; i++)
                {
                    if (cards[i].Value == cards[i + 1].Value)
                        return true;
                }
                return false;
                */
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
            PokerRank theRank = PokerRank.Unknown;

            //Check for poker hand ranks from highest to lowest
            //Unfortunately can't use switch since different properties are tested, not the same value
            if (IsRoyalFlush)
                theRank = PokerRank.RoyalFlush;
            else if (IsStraightFlush)
                theRank = PokerRank.StraightFlush;
            else if (IsFourOfAKind)
                theRank = PokerRank.FourOfAKind;
            else if (IsFullHouse)
                theRank = PokerRank.FullHouse;
            else if (IsFlush)
                theRank = PokerRank.Flush;
            else if (IsStraight)
                theRank = PokerRank.Straight;
            else if (IsThreeOfAKind)
                theRank = PokerRank.ThreeOfAKind;
            else if (IsTwoPair)
                theRank = PokerRank.TwoPair;
            else if (IsPair)
                theRank = PokerRank.Pair;
            else
                theRank = PokerRank.HighCard;


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
