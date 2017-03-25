using SyncAut.Common.Automaton.Algorithms;

namespace SyncAut.Common.Automaton
{
	public static class AutomatonExtensions
	{
		public static bool IsSynchronizedByWord(this Automaton automaton, string word)
		{
			return new CheckIsSynchronizingWordAlgorithm(automaton).Check(word);
		}
	}
}