namespace ThreeInLine.Services.Container
{
	public interface ICommand
	{
		void Execute();
	}

	public interface ICommand<T>
	{
		void Execute(T context);
	}
}