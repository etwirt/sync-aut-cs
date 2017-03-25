using System.Collections.Generic;
using JetBrains.Annotations;

namespace SyncAut.Common.Automaton.Builders
{
	public class StateBuilder
	{
		private string stateTitle;
		private int stateIndex;
		private readonly List<Jump> jumps = new List<Jump>();

		public StateBuilder WithIndex(int index)
		{
			stateIndex = index;
			return this;
		}

		public StateBuilder WithTitle([CanBeNull] string title)
		{
			stateTitle = title;
			return this;
		}

		public StateBuilder WithJump(char letter, int state)
		{
			jumps.Add(new Jump(letter, state));
			return this;
		}

		public State Build()
		{
			return new State(stateIndex, jumps, stateTitle);
		}
	}
}