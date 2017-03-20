using JetBrains.Annotations;

namespace SyncAut.Common.Automaton
{
	public class State
	{
		public int Index { get; private set; }

		private readonly JumpTable jumpTable;

		public string Title { get; private set; }

		public State(int index, JumpTable jumpTable, [CanBeNull] string title)
		{
			Index = index;
			Title = title ?? "<No title>";
			this.jumpTable = new JumpTable(jumpTable.GetAllJumps());
		}

		public int Jump(char letter)
		{
			return jumpTable.Jump(letter);
		}
	}
}