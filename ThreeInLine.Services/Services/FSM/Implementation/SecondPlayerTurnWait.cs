using ThreeInLine.Services.Container;

namespace ThreeInLine.Services.FSM.Implementation
{
	internal class SecondPlayerTurnWait : TurnWaitBase
	{
		public SecondPlayerTurnWait(IContainer container) : base(container) { }

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