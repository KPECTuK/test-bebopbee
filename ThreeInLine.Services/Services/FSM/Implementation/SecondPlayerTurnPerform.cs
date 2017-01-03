using ThreeInLine.Services.Container;

namespace ThreeInLine.Services.FSM.Implementation
{
	internal class SecondPlayerTurnPerform : TurnPerformBase
	{
		public SecondPlayerTurnPerform(IContainer container) : base(container) { }

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