using ThreeInLine.Services.Container;

namespace ThreeInLine.Services.FSM.Implementation
{
	internal class FirstPlayerTurnWait : TurnWaitBase
	{
		public FirstPlayerTurnWait(IContainer container) : base(container) { }

		private TurnPerformBase _performing;

		protected override int PlayerIndex => 1;
		protected override TurnPerformBase PerformingState => _performing ?? (_performing = Container.Resolve<FirstPlayerTurnPerform>());
	}
}