using System.Collections.Generic;
using JetBrains.Annotations;

namespace SyncAut.Common.Automaton
{
	public class State
	{
		public int Index { get; private set; }

		private readonly JumpTable jumpTable;

		public string Title { get; private set; }

		public State(int index, JumpTable jumpTable, [CanBeNull] string title)
			: this(index, jumpTable.GetAllJumps(), title)
		{
		}

		public State(int index, IEnumerable<Jump> jumps, [CanBeNull] string title)
		{
			Index = index;
			Title = title ?? "<No title>";
			jumpTable = new JumpTable(jumps);
		}

		public int Jump(char letter)
		{
			return jumpTable.Jump(letter);
		}

		public int? TryJump(char letter)
		{
			return jumpTable.TryJump(letter);
		}
	}
}