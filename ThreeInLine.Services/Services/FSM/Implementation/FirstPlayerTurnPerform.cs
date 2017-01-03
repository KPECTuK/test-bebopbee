using ThreeInLine.Services.Container;

namespace ThreeInLine.Services.FSM.Implementation
{
	internal class FirstPlayerTurnPerform : TurnPerformBase
	{
		public FirstPlayerTurnPerform(IContainer container) : base(container) { }

		public override bool Rising()
		{
			return false;
		}

		public override bool Idle()
		{
			return false;
		}

		public override bool Fading()
		{
			return false;
		}
	}
}