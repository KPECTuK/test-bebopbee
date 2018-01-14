using System;
using System.Collections;

namespace ThreeInLine.Services.FSM
{
	internal class StateWrapper : IEnumerator
	{
		private readonly IState _state;
		private int _executing;
		private readonly Func<bool>[] _executable;

		public StateWrapper(IState state)
		{
			_state = state;
			_executable = new Func<bool>[]
			{
				ResetState,
				_state.Rising,
				_state.Idle,
				_state.Fading,
			};
		}

		private bool ResetState()
		{
			_executing = 0;
			_state.ResetState();
			return false;
		}

		// IEnumerator
		public bool MoveNext()
		{
			if(_executing < _executable.Length && _executable[_executing]())
				return true;
			_executing += 1;
			if(_executing < _executable.Length)
				return true;
			Reset();
			return false;
		}

		// IEnumerator
		public void Reset()
		{
			ResetState();
		}

		// IEnumerator
		public object Current {get { return _state; } }
	}
}