using ThreeInLine.Services.Container;

namespace ThreeInLine.Services.FSM
{
	internal abstract class TurnPerformBase : StateTemplate
	{
		protected TurnPerformBase(IContainer container) : base(container) { }
	}
}