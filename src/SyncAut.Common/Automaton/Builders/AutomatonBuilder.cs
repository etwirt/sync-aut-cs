using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace SyncAut.Common.Automaton.Builders
{
	public class AutomatonBuilder
	{
		private readonly List<State> states = new List<State>();
		private StateBuilder stateBuilder;

		public AutomatonBuilder BeginState(int index, [CanBeNull] string title)
		{
			if (stateBuilder != null)
				throw new InvalidOperationException("Preparing of state already started");
			stateBuilder = new StateBuilder();
			stateBuilder.WithIndex(index);
			stateBuilder.WithTitle(title);
			return this;
		}

		public AutomatonBuilder WithJump(char letter, int index)
		{
			stateBuilder.WithJump(letter, index);
			return this;
		}

		public AutomatonBuilder EndState()
		{
			if (stateBuilder == null)
				throw new InvalidOperationException("State is not prepared");
			states.Add(stateBuilder.Build());
			stateBuilder = null;
			return this;
		}

		public Automaton Build()
		{
			if (stateBuilder != null)
				throw new InvalidOperationException("Preparing of state has began but not finished");
			return new Automaton(states);
		}
	}
}