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
        public PokerRank Rank => (_rank == PokerRank.Unknown)? DetermineRank() : _rank;

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
            //Last index will be the same as the first if there are no duplicates
            lastValueIdx = firstValueIdx;

            //HighCard defaults to the last card if there are no identical values
            HighCard = cards[^1];

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
            
        }

        /// <summary>
        /// Tests to see if all cards have the same color
        /// </summary>
        /// <param name="HighCard">Highest card in the PokerHand</param>
        /// <returns>True or false depending on the result</returns>
        private bool IsSameColor(out PlayingCard HighCard)
        {
            //HighCard will be the last card no matter what
            HighCard = cards[cards.Count - 1];
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
            HighCard = cards[^1];
            //If any two consecutive cards doesn't have a difference of 1 in value, return false
            //Essentially tests for a Straight
            for (int i = 0; i < cards.Count - 1; i++)
            {
                if ((int)cards[i].Value + 1 != (int)cards[i + 1].Value)
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
                //I'm using HighCard here, but I could have just used cards[^1].Value... It's the same thing.
                if (IsSameColor(out _) && IsConsecutive(out _rankHigh))
                {
                    if (cards[^1].Value == PlayingCardValue.Ace)
                        return true;
                }
                return false;
            }
        }
        private bool IsStraightFlush
        {
            get
            {
                if (IsSameColor(out _) && IsConsecutive(out _rankHigh))
                    return true;
                return false;
            }
        }
        private bool IsFourOfAKind
        {
            get
            {
                //Since there are 5 cards, only the 0th and 1st index need to be tested with NrSameValue
                if (NrSameValue(0, out _, out _rankHigh) == 3 || NrSameValue(1, out _, out _rankHigh) == 3)
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
                //If the first 3 cards are the same, and the last 2 cards are the same, return true
                if (NrSameValue(0, out int lastValueIdx1, out _rankHigh) == 2 && NrSameValue(lastValueIdx1 + 1, out _, out _) == 1)
                    return true;

                //If the first 2 cards are the same, and the last 3 cards are the same, return true
                if (NrSameValue(0, out int lastValueIdx2, out _) == 1 && NrSameValue(lastValueIdx2 +1, out _, out _rankHigh) == 2)
                    return true;

                //Otherwise, return false
                return false;

            }
        }
        private bool IsFlush
        {
            get
            {
                return IsSameColor(out _rankHigh);
            }
        }
        private bool IsStraight
        {
            get
            {
                return IsConsecutive(out _rankHigh);
            }
        }
        private bool IsThreeOfAKind
        {
            get
            {
                //Since there are only 5 cards, it's unnecessary to test the last two cards.
                if (NrSameValue(0, out _, out _rankHigh) == 3 || NrSameValue(1, out _, out _rankHigh) == 3 || NrSameValue(2, out _, out _rankHigh) == 3)
                    return true;
                return false;
            }
        }
        private bool IsTwoPair
        {
            get
            {
                //private int NrSameValue(int firstValueIdx, out int lastValueIdx, out PlayingCard HighCard)
                int lastValueIdx = 0;
                bool firstPair = false;
                PlayingCard highCard = null;
                PlayingCard highCard1 = null;

                //Loop through all cards except the last one to check for duplicates
                for(int i = 0; i < cards.Count - 1; i++)
                {
                    //How many duplicates in value does cards[i] have?
                    int duplicates = NrSameValue(i, out lastValueIdx, out highCard);

                    //If another pair was found before, and there is a second pair now, return true
                    if (duplicates == 1 && firstPair)
                    {
                        _rankHigh = cards[^1];
                        _rankHighPair1 = highCard1;
                        _rankHighPair2 = highCard;
                        return true;
                    }

                    //There is at least one pair if the number of duplicates is exactly 1
                    if (duplicates == 1 && !firstPair) 
                    {
                        firstPair = true;

                        //Save for next loop
                        highCard1 = highCard;

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

                for (int i = 0; i < cards.Count - 1; i++)
                {
                    //Specifically testing for pairs and not for triple cards
                    //Will return true as soon as 1 pair is found, ignoring any potential card combinations on higher indexes in the PokerHand.
                    if(NrSameValue(i, out _, out _rankHigh) == 1)
                    {
                        return true;
                    }
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
            //PokerRank theRank = PokerRank.Unknown;

            //Check for poker hand ranks from highest to lowest
            //Unfortunately can't use switch since different properties are tested, not the same value

            if (IsRoyalFlush)
                return PokerRank.RoyalFlush;
            if (IsStraightFlush)
                return PokerRank.StraightFlush;
            if (IsFourOfAKind)
                return PokerRank.FourOfAKind;
            if (IsFullHouse)
                return PokerRank.FullHouse;
            if (IsFlush)
                return PokerRank.Flush;
            if (IsStraight)
                return PokerRank.Straight;
            if (IsThreeOfAKind)
                return PokerRank.ThreeOfAKind;
            if (IsTwoPair)
                return PokerRank.TwoPair;
            if (IsPair)
                return PokerRank.Pair;

            return PokerRank.HighCard;

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
