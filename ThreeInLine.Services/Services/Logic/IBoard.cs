namespace ThreeInLine.Services.Logic
{
	public interface IBoard
	{
		bool Turn(int player, int from, int to);
		ILine GetLineComplete();
	}
}