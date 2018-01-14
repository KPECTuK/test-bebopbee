namespace ThreeInLine.Services.Container
{
	public interface IProvider<T>
	{
		T Get();
	}
}