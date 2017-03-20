using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace SyncAut.Common.Automaton
{
	public class Automaton
	{
		private readonly ConcurrentDictionary<int, State> states = new ConcurrentDictionary<int, State>();

		public int StatesCount => states.Count;

		public Automaton(IEnumerable<State> states)
		{
			foreach (var state in states)
			{
				SetState(state);
			}
		}

		public Automaton(params State[] states)
		{
			foreach (var state in states)
			{
				SetState(state);
			}
		}

		private void SetState([NotNull] State state)
		{
			states.AddOrUpdate(state.Index, state, (index, state1) =>
			{
				throw new InvalidOperationException($"Duplication of state with index: {index}");
			});
		}

		[NotNull]
		public State GetState(int index)
		{
			var state = states[index];
			if (state == null)
				throw new InvalidOperationException();
			return state;
		}

		[CanBeNull]
		public State FindState(int index)
		{
			State state;
			return states.TryGetValue(index, out state) ? state : null;
		}

		public int Jump(int fromState, char letter)
		{
			return GetState(fromState).Jump(letter);
		}
	}
}