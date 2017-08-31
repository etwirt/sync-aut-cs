﻿using System.Collections.Generic;
using System.Linq;

namespace SyncAut.Common.Automaton
{
	public class JumpTable
	{
		private readonly Dictionary<char, int> letterActions = new Dictionary<char, int>();

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
			letterActions.Add(jump.Letter, jump.State);
		}

		public int Jump(char letter)
		{
			return letterActions[letter];
		}

		public int? TryJump(char letter)
		{
			int value;
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