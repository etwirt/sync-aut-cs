using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace SyncAut.Common.Automaton
{
	public class Automaton
	{
		private readonly ConcurrentDictionary<string, State> states = new ConcurrentDictionary<string, State>();

		public int StatesCount => states.Count;

		public char[] Letters { get; }

		public Automaton(params State[] states)
		{
			foreach (var state in states)
			{
				SetState(state);
			}

			var letters = states.SelectMany(x => x.GetPossibleLetters()).Distinct().ToArray();
			Letters = letters;
		}

		private void SetState(State state)
		{
			states.AddOrUpdate(state.Id, state, (id, state1) =>
			{
				throw new InvalidOperationException($"Duplication of state with id: {id}");
			});
		}

		public State GetState(string id)
		{
			var state = states[id];
			if (state == null)
				throw new InvalidOperationException();
			return state;
		}

		[CanBeNull]
		public State FindState(string id)
		{
			State state;
			return states.TryGetValue(id, out state) ? state : null;
		}

		public string Jump(string fromState, char letter)
		{
			return GetState(fromState).Jump(letter);
		}

		[CanBeNull]
		public string TryJump(string fromState, char letter)
		{
			return GetState(fromState).TryJump(letter);
		}

		public List<State> GetAllStates()
		{
			return states.Values.ToList();
		}
	}
}