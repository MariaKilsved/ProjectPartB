using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPartB_B1
{
	public class PlayingCard:IComparable<PlayingCard>, IPlayingCard
	{
		public PlayingCardColor Color { get; init; }
		public PlayingCardValue Value { get; init; }

		#region IComparable Implementation
		//Need only to compare value in the project
		public int CompareTo(PlayingCard card1)
        {
			if (this.Value.CompareTo(card1.Value) < 0)
				return -1;
			else if (this.Value.CompareTo(card1.Value) == 0)
				return 0;
			else
				return 1;
        }
		#endregion

        #region ToString() related
		private string ShortDescription
		{
			//Use switch statment or switch expression
			//https://en.wikipedia.org/wiki/Playing_cards_in_Unicode
			get
			{
				return Color switch
				{
					PlayingCardColor.Spades => $"\u2660 {Value}",
					PlayingCardColor.Hearts => $"\u2665 {Value}",
					PlayingCardColor.Diamonds => $"\u2666 {Value}",
					PlayingCardColor.Clubs => $"\u2663 {Value}",
					_ => throw new ArgumentOutOfRangeException(nameof(Color), $"Not expected color value: {Color}")

				};
			}
		}

		public override string ToString() => ShortDescription;
        #endregion
    }
}
