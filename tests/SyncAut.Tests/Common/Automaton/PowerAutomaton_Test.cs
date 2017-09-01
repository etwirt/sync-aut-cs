using NUnit.Framework;

namespace SyncAut.Common.Automaton
{
	[TestFixture]
	public class PowerAutomaton_Test
	{
		[Test]
		public void Test()
		{
			var automaton = new Automaton(
				new State("1", new JumpTable(new Jump('a', "2")), "state1"),
				new State("2", new JumpTable(new Jump('a', "1")), "state2"));
			Assert.That(automaton.StatesCount, Is.EqualTo(2));

			var powerAutomaton = new PowerAutomaton(automaton);
			Assert.That(powerAutomaton.StatesCount, Is.EqualTo(4));

		}
	}
}