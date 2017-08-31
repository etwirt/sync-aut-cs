using System.Linq;

namespace SyncAut.Common.Automaton.Algorithms
{
	public class SynchronizingWordChecker
	{
		private readonly Automaton automaton;

		public SynchronizingWordChecker(Automaton automaton)
		{
			this.automaton = automaton;
		}

		public bool Check(string word)
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