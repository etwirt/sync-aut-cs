using System;
using NUnit.Framework;

namespace SyncAut.Common.Automaton
{
	[TestFixture]
	public class Automaton_Test
	{
		[Test]
		public void CreateEmptyAutomaton_CheckStatesCount()
		{
			var automaton = new Automaton();
			Assert.That(automaton.StatesCount, Is.EqualTo(0));
		}

		[Test]
		public void FindNotExistentState_ShouldReturnNull()
		{
			var automaton = new Automaton(new State(2, new JumpTable(), "state2"));
			Assert.Null(automaton.FindState(1));
		}

		[Test]
		public void CreateAutomatonWithStates_CheckGetState()
		{
			var automaton = new Automaton(
				new State(1, new JumpTable(new Jump('a', 2)), "state1"),
				new State(2, new JumpTable(new Jump('a', 1)), "state2"));
			Assert.That(automaton.StatesCount, Is.EqualTo(2));

			var state1 = automaton.GetState(1);
			Assert.NotNull(state1);
			Assert.That(state1.Index, Is.EqualTo(1));
			Assert.That(state1.Title, Is.EqualTo("state1"));

			var state2 = automaton.GetState(2);
			Assert.NotNull(state2);
			Assert.That(state2.Index, Is.EqualTo(2));
			Assert.That(state2.Title, Is.EqualTo("state2"));
		}

		[Test]
		public void CreateAutomatonWithSameStateIndex_Error()
		{
			Assert.Throws<InvalidOperationException>(() => new Automaton(
				new State(1, new JumpTable(new Jump('a', 2)), "state1"),
				new State(1, new JumpTable(new Jump('a', 1)), "state2")));
		}

		[Test]
		public void CheckJumps()
		{
			var automaton = new Automaton(
				new State(1, new JumpTable(new Jump('a', 2), new Jump('b',  1)), "state1"),
				new State(2, new JumpTable(new Jump('a', 1), new Jump('c', 1)), "state2"));
			
			Assert.That(automaton.Jump(1, 'a'), Is.EqualTo(2));
			Assert.That(automaton.Jump(1, 'b'), Is.EqualTo(1));
			Assert.That(automaton.Jump(2, 'a'), Is.EqualTo(1));
			Assert.That(automaton.Jump(2, 'c'), Is.EqualTo(1));
		}
	}
}