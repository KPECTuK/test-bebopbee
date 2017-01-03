namespace ThreeInLine.Services.Repository
{
	public interface IBoard
	{
		bool Turn(int player, IntVector from, IntVector to);
		ILine GetLineComplete();
	}
}