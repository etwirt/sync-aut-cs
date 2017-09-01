using NUnit.Framework;
using SyncAut.Common.Automaton.Builders;

namespace SyncAut.Common.Automaton.Algorithms
{
	[TestFixture]
	public class SynchronizingWordChecker_Test
	{
		[Test]
		public void EmptyAutomaton()
		{
			var a = new Automaton();
			Assert.True(IsSynchronizing(a, "a"));
			Assert.True(IsSynchronizing(a, ""));
		}

		[Test]
		public void OneStateAutomaton()
		{
			var a = new AutomatonBuilder()
				.BeginState("1")
				.WithJump('a', "1")
				.EndState()
				.Build();
			Assert.True(IsSynchronizing(a, "a"));
			Assert.True(IsSynchronizing(a, ""));
		}

		[Test]
		public void SeveralStatesAutomatonWithSynchronizingWord()
		{
			var a = new AutomatonBuilder()
				.BeginState("1")
				.WithJump('a', "2")
				.WithJump('b', "3")
				.EndState()
				.BeginState("2")
				.WithJump('a', "3")
				.WithJump('b', "3")
				.EndState()
				.BeginState("3")
				.WithJump('a', "2")
				.WithJump('b', "3")
				.EndState()
				.Build();

			Assert.True(IsSynchronizing(a, "aab"));
			Assert.True(IsSynchronizing(a, "aaba"));
			Assert.True(IsSynchronizing(a, "b"));
			Assert.True(IsSynchronizing(a, "ba"));

			Assert.False(IsSynchronizing(a, ""));
			Assert.False(IsSynchronizing(a, "a"));
		}

		private static bool IsSynchronizing(Automaton automaton, string word)
		{
			return new SynchronizingWordChecker(automaton).Check(word);
		}
	}
}