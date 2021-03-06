﻿using System;
using System.Diagnostics;

namespace AIGames.HeadsUpOmaha.Game
{
	/// <summary>Represents the state of a player.</summary>
	[Serializable, DebuggerDisplay("{DebugToString()}")]
	public class PlayerState
	{
		/// <summary>Creates a new player state.</summary>
		public PlayerState()
		{
			this.Hand = Cards.Empty;
		}

		/// <summary>Creates a new player state based on the settings.</summary>
		public PlayerState(Settings settings): this()
		{
			Update(settings);
		}

		/// <summary>The stack of the player.</summary>
		public int Stack { get; set; }
		
		/// <summary>The chips in the pot of the player.</summary>
		public int Pot { get; set; }

		/// <summary>The hand of the player.</summary>
		public Cards Hand { get; set; }

		/// <summary>The time left for of the player.</summary>
		public TimeSpan TimeBank { get; set; }

		/// <summary>Creates a copy of the player hand.</summary>
		public PlayerState Copy()
		{
			var copy = new PlayerState()
			{
				Stack = this.Stack,
				Pot = this.Pot,
				Hand = this.Hand.Copy(),
			};

			return copy;
		}

		/// <summary>Updates the player state based on the settings.</summary>
		public void Update(Settings settings)
		{
			this.TimeBank = TimeSpan.FromMilliseconds(settings.TimeBank);
			this.Stack = settings.StartingStack;
		}

		/// <summary>Set the stack.</summary>
		public void SetStack(int stack)
		{
			this.Stack = stack;
			this.Pot = 0;
		}

		/// <summary>Handle a post to the pot.</summary>
		public void Post(int post)
		{
			this.Stack -= post;
			this.Pot += post;
		}

		/// <summary>Handle a call.</summary>
		public void Call(int amount)
		{
			this.Stack -= amount;
			this.Pot += amount;
		}

		/// <summary>Handle a raise.</summary>
		public void Raise(int raise)
		{
			this.Stack -= raise;
			this.Pot += raise;
		}

		/// <summary>Rest the player state.</summary>
		/// <remarks>
		/// Resets the hand.
		/// </remarks>
		public void Reset()
		{
			this.Hand = Cards.Empty;
		}

		/// <summary>Represents a player state as a debug string.</summary>
		[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
		private string DebugToString()
		{
			return String.Format("Player: {0} ({1}) {2:f}, Time: {3:0,000}", this.Stack, this.Pot, this.Hand, this.TimeBank.Milliseconds);
		}
	}
}
