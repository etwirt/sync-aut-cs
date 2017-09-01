using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SyncAut.Common.Automaton.Builders;

namespace SyncAut.Common.Automaton
{
	public class PowerAutomaton
	{
		private Automaton sourceAutomaton;
		private Automaton powerAutomaton;
		private string[] sourceStates;
		private readonly Dictionary<string, int> fromIdToIndex = new Dictionary<string, int>();
		private AutomatonBuilder automatonBuilder;
		public int StatesCount { get { return powerAutomaton.StatesCount; } }

		public PowerAutomaton(Automaton automaton)
		{
			Build(automaton);
		}

		private void Build(Automaton automaton)
		{
			sourceAutomaton = automaton;
			sourceStates = automaton.GetAllStates().Select(x => x.Id).ToArray();
			for (var i = 0; i < sourceStates.Length; i++)
			{
				fromIdToIndex[sourceStates[i]] = i;
			}

			automatonBuilder = new AutomatonBuilder();
			var set = new BitArray(automaton.StatesCount);
			set.SetAll(false);

			Fill(set, 0, AddState);
			powerAutomaton = automatonBuilder.Build();
		}

		private void AddState(BitArray set)
		{
			var builder = automatonBuilder.BeginState(CreateStateId(set), CreateStateId(set));
			foreach (var letter in sourceAutomaton.Letters)
			{
				var newState = MakeJump(set, letter);
				builder.WithJump(letter, CreateStateId(newState));
			}
			builder.EndState();
		}

		private BitArray MakeJump(BitArray state, char letter)
		{
			var newState = new BitArray(state.Count);
			newState.SetAll(false);
			for (var i = 0; i < state.Count; i++)
			{
				var stateId = sourceAutomaton.TryJump(sourceStates[i], letter);
				if (stateId == null)
				{
					continue;
				}
				newState[fromIdToIndex[stateId]] = true;
			}
			return newState;
		}

		private void Fill(BitArray set, int filled, Action<BitArray> action)
		{
			if (filled == set.Count)
			{
				action(set);
			}
			set[filled] = false;
			Fill(set, filled + 1, action);
			set[filled] = true;
			Fill(set, filled + 1, action);
		}

		private static string CreateStateId(BitArray set)
		{
			var result = new StringBuilder(set.Count);
			foreach (bool flag in set)
			{
				result.Append(flag ? '1' : '0');
			}
			return result.ToString();
		}
	}
}