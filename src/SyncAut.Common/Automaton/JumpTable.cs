using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace SyncAut.Common.Automaton
{
	public class JumpTable
	{
		private readonly Dictionary<char, int> letterActions = new Dictionary<char, int>();

		public JumpTable([NotNull] IEnumerable<Jump> jumps)
		{
			foreach (var jump in jumps)
			{
				AddJump(jump);
			}
		}

		public JumpTable([NotNull] params Jump[] jumps)
		{
			foreach (var jump in jumps)
			{
				AddJump(jump);
			}
		}

		private void AddJump([NotNull] Jump jump)
		{
			letterActions.Add(jump.Letter, jump.State);
		}

		public int Jump(char letter)
		{
			return letterActions[letter];
		}

		[NotNull]
		public List<Jump> GetAllJumps()
		{
			return letterActions.Select(x => new Jump(x.Key, x.Value)).ToList();
		}
	}
}