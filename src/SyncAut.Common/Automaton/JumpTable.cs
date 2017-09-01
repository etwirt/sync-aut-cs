using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace SyncAut.Common.Automaton
{
	public class JumpTable
	{
		private readonly Dictionary<char, string> letterActions = new Dictionary<char, string>();

		public JumpTable(IEnumerable<Jump> jumps)
		{
			foreach (var jump in jumps)
			{
				AddJump(jump);
			}
		}

		public JumpTable(params Jump[] jumps)
		{
			foreach (var jump in jumps)
			{
				AddJump(jump);
			}
		}

		private void AddJump(Jump jump)
		{
			letterActions.Add(jump.Letter, jump.StateId);
		}

		public string Jump(char letter)
		{
			return letterActions[letter];
		}

		[CanBeNull]
		public string TryJump(char letter)
		{
			string value;
			if (letterActions.TryGetValue(letter, out value))
				return value;
			return null;
		}

		public List<Jump> GetAllJumps()
		{
			return letterActions.Select(x => new Jump(x.Key, x.Value)).ToList();
		}
	}
}