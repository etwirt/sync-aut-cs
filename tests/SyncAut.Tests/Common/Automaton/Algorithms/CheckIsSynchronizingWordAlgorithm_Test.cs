using NUnit.Framework;
using SyncAut.Common.Automaton.Builders;

namespace SyncAut.Common.Automaton.Algorithms
{
	[TestFixture]
	public class CheckIsSynchronizingWordAlgorithm_Test
	{
		[Test]
		public void EmptyAutomaton()
		{
			var a = new Automaton();
			Assert.True(a.IsSynchronizedByWord("a"));
			Assert.True(a.IsSynchronizedByWord(""));
		}

		[Test]
		public void OneStateAutomaton()
		{
			var a = new AutomatonBuilder()
				.BeginState(1)
				.WithJump('a', 1)
				.EndState()
				.Build();
			Assert.True(a.IsSynchronizedByWord("a"));
			Assert.True(a.IsSynchronizedByWord(""));
		}

		[Test]
		public void SeveralStatesAutomatonWithSynchronizingWord()
		{
			var a = new AutomatonBuilder()
				.BeginState(1)
				.WithJump('a', 2)
				.WithJump('b', 3)
				.EndState()
				.BeginState(2)
				.WithJump('a', 3)
				.WithJump('b', 3)
				.EndState()
				.BeginState(3)
				.WithJump('a', 2)
				.WithJump('b', 3)
				.EndState()
				.Build();

			Assert.True(a.IsSynchronizedByWord("aab"));
			Assert.True(a.IsSynchronizedByWord("aaba"));
			Assert.True(a.IsSynchronizedByWord("b"));
			Assert.True(a.IsSynchronizedByWord("ba"));

			Assert.False(a.IsSynchronizedByWord(""));
			Assert.False(a.IsSynchronizedByWord("a"));
		}
	}
}