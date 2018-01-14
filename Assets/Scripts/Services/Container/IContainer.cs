namespace ThreeInLine.Services.Container
{
	public interface IContainer
	{
		T Resolve<T>() where T : class;
		void Release();
		void RegisterInstance(object instance);
		void RegisterInstance<T>(object instance);
		void RegisterProvider<T>(object provider);
	}
}