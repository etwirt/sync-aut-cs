namespace SyncAut.Common.Automaton
{
	public class Jump
	{
		public char Letter { get; private set; }
		public int State { get; private set; }

		public Jump(char letter, int state)
		{
			Letter = letter;
			State = state;
		}
	}
}