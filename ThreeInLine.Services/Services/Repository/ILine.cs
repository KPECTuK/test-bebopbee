namespace ThreeInLine.Services.Repository
{
	public interface ILine
	{
		int this[int index] { get; }
		int Length { get; }
	}
}