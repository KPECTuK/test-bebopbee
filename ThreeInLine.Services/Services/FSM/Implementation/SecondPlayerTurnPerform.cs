using ThreeInLine.Services.Container;

namespace ThreeInLine.Services.FSM.Implementation
{
	internal class SecondPlayerTurnPerform : TurnPerformBase
	{
		public SecondPlayerTurnPerform(IContainer container) : base(container) { }
		protected override int PlayerIndex => -1;
	}
}