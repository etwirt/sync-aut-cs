using System.Collections.Generic;
using JetBrains.Annotations;

namespace SyncAut.Common.Automaton
{
	public class State
	{
		public string Id { get; private set; }

		private readonly JumpTable jumpTable;

		public string Title { get; private set; }

		public State(string id, JumpTable jumpTable, [CanBeNull] string title)
			: this(id, jumpTable.GetAllJumps(), title)
		{
		}

		public State(string id, IEnumerable<Jump> jumps, [CanBeNull] string title)
		{
			Id = id;
			Title = title ?? "<No title>";
			jumpTable = new JumpTable(jumps);
		}

		public string Jump(char letter)
		{
			return jumpTable.Jump(letter);
		}

		[CanBeNull]
		public string TryJump(char letter)
		{
			return jumpTable.TryJump(letter);
		}
	}
}