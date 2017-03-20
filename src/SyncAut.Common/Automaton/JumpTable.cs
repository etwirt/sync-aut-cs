using System.Collections.Generic;

namespace SyncAut.Common.Automaton
{
	public class JumpTable
	{
		private readonly Dictionary<char, int> letterActions = new Dictionary<char, int>();

		public void AddJump(char letter, int newStateIndex)
		{
			letterActions.Add(letter, newStateIndex);
		}

		public int Jump(char letter)
		{
			return letterActions[letter];
		}
	}
}