namespace ThreeInLine.Services.FSM
{
	public interface IState
	{
		void ResetState();
		bool Rising();
		bool Idle();
		bool Fading();
	}
}
