using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace SyncAut.Common.Automaton
{
	[TestFixture]
	public class JumpTable_Test
	{
		private JumpTable jumpTable;

		[SetUp]
		public void SetUp()
		{
			jumpTable = new JumpTable();
		}

		[Test]
		public void SetJumps_ReadJumps()
		{
			jumpTable.AddJump('a', 10);
			jumpTable.AddJump('b', 20);

			Assert.That(jumpTable.Jump('a'), Is.EqualTo(10));
			Assert.That(jumpTable.Jump('b'), Is.EqualTo(20));
		}

		[Test]
		public void JumpNotSet_Error()
		{
			Assert.Throws<KeyNotFoundException>(() => jumpTable.Jump('a'));
		}

		[Test]
		public void AddJumpByLetterTwice_Error()
		{
			jumpTable.AddJump('a', 5);
			Assert.Throws<ArgumentException>(() => jumpTable.AddJump('a', 10));
		}
	}
}