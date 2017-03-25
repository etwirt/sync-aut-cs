using System;
using NUnit.Framework;

namespace SyncAut.Common.Automaton.Builders
{
	[TestFixture]
	public class AutomatonBuilder_Test
	{
		[Test]
		public void NoStates_Build_NoError()
		{
			var automaton = new AutomatonBuilder().Build();
			Assert.That(automaton.StatesCount, Is.EqualTo(0));
		}

		[Test]
		public void BuildWithStates_Check()
		{
			var automaton = new AutomatonBuilder()
				.BeginState(1, "title1")
				.WithJump('a', 1)
				.WithJump('b', 2)
				.WithJump('c', 3)
				.EndState()
				.BeginState(2, "title2")
				.WithJump('c', 2)
				.WithJump('a', 3)
				.EndState()
				.Build();

			Assert.That(automaton.StatesCount, Is.EqualTo(2));

			var state1 = automaton.GetState(1);
			Assert.That(state1.Index, Is.EqualTo(1));
			Assert.That(state1.Title, Is.EqualTo("title1"));
			Assert.That(state1.Jump('a'), Is.EqualTo(1));
			Assert.That(state1.Jump('b'), Is.EqualTo(2));
			Assert.That(state1.Jump('c'), Is.EqualTo(3));

			var state2 = automaton.GetState(2);
			Assert.That(state2.Index, Is.EqualTo(2));
			Assert.That(state2.Title, Is.EqualTo("title2"));
			Assert.That(state2.Jump('c'), Is.EqualTo(2));
			Assert.That(state2.Jump('a'), Is.EqualTo(3));
			Assert.Null(state2.TryJump('b'));

			var state3 = automaton.FindState(3);
			Assert.Null(state3);
		}

		[Test]
		public void NotFinishedStateBuilding_Build_Error()
		{
			var builder = new AutomatonBuilder()
				.BeginState(1, "title");
			Assert.Throws<InvalidOperationException>(() => builder.Build());
		}

		[Test]
		public void StartStateBuilding_StartNewStateBuilding_Error()
		{
			var builder = new AutomatonBuilder()
				.BeginState(1, "title");
			Assert.Throws<InvalidOperationException>(() => builder.BeginState(2, "title"));
		}
	}
}