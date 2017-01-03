using ThreeInLine.Services.Container;

namespace ThreeInLine.Services.FSM
{
	internal abstract class TurnWaitBase : StateTemplate
	{
		protected TurnWaitBase(IContainer container) : base(container) { }
	}
}