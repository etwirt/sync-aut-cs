using System.Linq;
using JetBrains.Annotations;

namespace SyncAut.Common.Automaton.Algorithms
{
	public class CheckIsSynchronizingWordAlgorithm
	{
		private readonly Automaton automaton;

		public CheckIsSynchronizingWordAlgorithm([NotNull] Automaton automaton)
		{
			this.automaton = automaton;
		}

		public bool Check([NotNull] string word)
		{
			var leftStates = automaton.GetAllStates().Select(x => x.Index).ToList();
			if (leftStates.Count <= 1)
				return true;
			foreach (var letter in word)
			{
				var newLeftStates = leftStates.Select(x => automaton.Jump(x, letter)).Distinct().ToList();
				if (newLeftStates.Count <= 1)
					return true;
				leftStates = newLeftStates;
			}
			return false;
		}
	}
}