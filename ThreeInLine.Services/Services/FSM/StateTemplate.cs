using ThreeInLine.Services.Container;

namespace ThreeInLine.Services.FSM
{
	internal abstract class StateTemplate : IState
	{
		protected readonly IContainer Container;

		protected StateTemplate(IContainer container)
		{
			Container = container;
		}

		public abstract void ResetState();
		public abstract bool Rising();
		public abstract bool Idle();
		public abstract bool Fading();
	}
}