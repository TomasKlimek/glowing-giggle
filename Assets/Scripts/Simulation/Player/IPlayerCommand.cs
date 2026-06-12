namespace Quantum
{
	internal interface IPlayerCommand
	{
		internal void Process(Frame frame, in PlayerFilter filter);
	}
}
