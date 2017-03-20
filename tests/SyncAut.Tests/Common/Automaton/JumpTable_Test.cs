using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace SyncAut.Common.Automaton
{
	[TestFixture]
	public class JumpTable_Test
	{
		[Test]
		public void SetJumps_ReadJumps()
		{
			var jumpTable = new JumpTable(new Jump('a', 10), new Jump('b', 20));

			Assert.That(jumpTable.Jump('a'), Is.EqualTo(10));
			Assert.That(jumpTable.Jump('b'), Is.EqualTo(20));
		}

		[Test]
		public void JumpNotSet_Error()
		{
			var jumpTable = new JumpTable();
			Assert.Throws<KeyNotFoundException>(() => jumpTable.Jump('a'));
		}

		[Test]
		public void AddJumpByLetterTwice_Error()
		{
			var jumps = new[]
			{
				new Jump('a', 5),
				new Jump('a', 10)
			};
			Assert.Throws<ArgumentException>(() => new JumpTable(jumps));
		}
	}
}