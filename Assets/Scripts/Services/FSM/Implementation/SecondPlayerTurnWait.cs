using ThreeInLine.Services.Container;

namespace ThreeInLine.Services.FSM.Implementation
{
	internal class SecondPlayerTurnWait : TurnWaitBase
	{
		public SecondPlayerTurnWait(IContainer container) : base(container) { }

		private TurnPerformBase _performing;

		protected override int PlayerIndex {get { return -1; } }
		protected override TurnPerformBase PerformingState {get { return _performing ?? (_performing = Container.Resolve<SecondPlayerTurnPerform>()); } }
	}
}