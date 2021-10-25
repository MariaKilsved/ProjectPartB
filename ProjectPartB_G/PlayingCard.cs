using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPartB_B1
{
	public class PlayingCard:IComparable<PlayingCard>, IPlayingCard
	{
		/// <summary>
		/// Enum type representing a playing card color, also called suit
		/// </summary>
		public enum PlayingCardColor
		{
			Clubs = 0, Diamonds, Hearts, Spades         // Poker suit order, Spades highest
		}

		/// <summary>
		/// Enum type representing a playing card value
		/// </summary>
		public enum PlayingCardValue
		{
			Two = 2, Three, Four, Five, Six, Seven, Eight, Nine, Ten,
			Knight, Queen, King, Ace                // Poker Value order
		}

		public PlayingCardColor Color { get; init; }
		public PlayingCardValue Value { get; init; }

		#region IComparable Implementation
		//Need only to compare value in the project
		public int CompareTo(PlayingCard card1)
        {
			return 0;
        }
		#endregion

        #region ToString() related
		string ShortDescription
		{
			//Use switch statment or switch expression
			//https://en.wikipedia.org/wiki/Playing_cards_in_Unicode
			get
			{
				return Color switch
				{
					PlayingCardColor.Spades => "\u2660",
					PlayingCardColor.Hearts => "\u2665",
					PlayingCardColor.Diamonds => "\u2666",
					PlayingCardColor.Clubs => "\u2663",
					_ => throw new ArgumentOutOfRangeException(nameof(Color), $"Not expected color value: {Color}")

				};
			}
		}

		public override string ToString() => ShortDescription;
        #endregion
    }
}
