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
				_state.Rising,
				_state.Idle,
				_state.Fading,
			};
		}

		// IEnumerator
		public bool MoveNext()
		{
			if(_executing < _executable.Length && _executable[_executing]())
				return true;
			_executing += 1;
			return _executing < _executable.Length;
		}

		// IEnumerator
		public void Reset() { }
		// IEnumerator
		public object Current => _state;
	}
}