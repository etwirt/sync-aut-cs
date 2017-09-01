using System.Collections.Generic;
using JetBrains.Annotations;

namespace SyncAut.Common.Automaton.Builders
{
	public class StateBuilder
	{
		private string stateTitle;
		private string stateId;
		private readonly List<Jump> jumps = new List<Jump>();

		public StateBuilder WithId(string id)
		{
			stateId = id;
			return this;
		}

		public StateBuilder WithTitle([CanBeNull] string title)
		{
			stateTitle = title;
			return this;
		}

		public StateBuilder WithJump(char letter, string state)
		{
			jumps.Add(new Jump(letter, state));
			return this;
		}

		public State Build()
		{
			return new State(stateId, jumps, stateTitle);
		}
	}
}