using ThreeInLine.Services.Container;

namespace ThreeInLine.Services.FSM.Implementation
{
	internal class FirstPlayerTurnPerform : TurnPerformBase
	{
		public FirstPlayerTurnPerform(IContainer container) : base(container) { }
		protected override int PlayerIndex => 1;
	}
}