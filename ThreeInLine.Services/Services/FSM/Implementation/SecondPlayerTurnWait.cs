using ThreeInLine.Services.Container;

namespace ThreeInLine.Services.FSM.Implementation
{
	internal class SecondPlayerTurnWait : TurnWaitBase
	{
		public SecondPlayerTurnWait(IContainer container) : base(container) { }

		private TurnPerformBase _performing;

		protected override int PlayerIndex => -1;
		protected override TurnPerformBase PerformingState => _performing ?? (_performing = Container.Resolve<SecondPlayerTurnPerform>());
	}
}