using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThreeInLine.Services.FSM
{
	public interface IState
	{
		bool Rising();
		bool Idle();
		bool Fading();
	}
}
