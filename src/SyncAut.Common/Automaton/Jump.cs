namespace SyncAut.Common.Automaton
{
	public class Jump
	{
		public char Letter { get; private set; }
		public string StateId { get; private set; }

		public Jump(char letter, string stateId)
		{
			Letter = letter;
			StateId = stateId;
		}
	}
}