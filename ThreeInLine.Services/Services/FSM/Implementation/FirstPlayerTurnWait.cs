using ThreeInLine.Services.Container;

namespace ThreeInLine.Services.FSM.Implementation
{
	internal class FirstPlayerTurnWait : TurnWaitBase
	{
		public FirstPlayerTurnWait(IContainer container) : base(container) { }

		public override bool Rising()
		{
			return false;
		}

		public override bool Idle()
		{
			// shucking here
			return true;
		}

		public override bool Fading()
		{
			return false;
		}
	}
}