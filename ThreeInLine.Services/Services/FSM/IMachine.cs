using System.Collections;

namespace ThreeInLine.Services.FSM
{
	public interface IMachine : IEnumerator
	{
		void DropState(IState state);
	}
}