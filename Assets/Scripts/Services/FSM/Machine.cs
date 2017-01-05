using System;
using ThreeInLine.Services.Container;
using ThreeInLine.Services.Controllers;
using ThreeInLine.Services.FSM.Implementation;

namespace ThreeInLine.Services.FSM
{
	internal class Machine :
		IMachine,
		IProvider<FirstPlayerTurnWait>,
		IProvider<SecondPlayerTurnWait>,
		IProvider<FirstPlayerTurnPerform>,
		IProvider<SecondPlayerTurnPerform>,
		IProvider<NotInFocus>,
		IProvider<SelectorController>
	{
		private readonly StateWrapper[] _game;
		private readonly StateWrapper[] _focus;
		private int _gameCurrent;
		private int _focusCurrent;

		public Machine(IContainer container)
		{
			_game = new[]
			{
				new StateWrapper(new FirstPlayerTurnWait(container)),
				new StateWrapper(new FirstPlayerTurnPerform(container)),
				new StateWrapper(new SecondPlayerTurnWait(container)),
				new StateWrapper(new SecondPlayerTurnPerform(container)),
			};
			_focus = new[]
			{
				new StateWrapper(new NotInFocus(container)),
				new StateWrapper(FindComponent<SelectorController>()),
			};
		}

		private static TComponent FindComponent<TComponent>() where TComponent : MonoBehaviourExtended
		{
			return UnityEngine.Object.FindObjectOfType<TComponent>();
		}

		// IEnumerator
		public bool MoveNext()
		{
			if(_gameCurrent > _game.Length)
				return false;

			//focus
			_focusCurrent += _focus[_focusCurrent].MoveNext() ? 0 : 1;
			_focusCurrent = _focusCurrent < _focus.Length ? _focusCurrent : 0;
			// game
			_gameCurrent += _game[_gameCurrent].MoveNext() ? 0 : 1;
			_gameCurrent = _gameCurrent < _game.Length ? _gameCurrent : 0;

			return true;
		}

		public void DropState(IState state)
		{
			// search for the state and drop wrapper
			// don't need here, because of states are application wide persistent
		}

		// IEnumerator
		public void Reset() { }
		// IEnumerator
		public object Current { get { return _game[_gameCurrent]; } }

		// IProvider<FirstPlayerTurnWait>
		FirstPlayerTurnWait IProvider<FirstPlayerTurnWait>.Get()
		{
			return Array.Find(_game, _ => _.Current is FirstPlayerTurnWait).Current as FirstPlayerTurnWait;
		}

		// IProvider<SecondPlayerTurnWait>
		SecondPlayerTurnWait IProvider<SecondPlayerTurnWait>.Get()
		{
			return Array.Find(_game, _ => _.Current is SecondPlayerTurnWait).Current as SecondPlayerTurnWait;
		}

		// IProvider<FirstPlayerTurnPerform>
		FirstPlayerTurnPerform IProvider<FirstPlayerTurnPerform>.Get()
		{
			return Array.Find(_game, _ => _.Current is FirstPlayerTurnPerform).Current as FirstPlayerTurnPerform;
		}

		// IProvider<SecondPlayerTurnPerform>
		SecondPlayerTurnPerform IProvider<SecondPlayerTurnPerform>.Get()
		{
			return Array.Find(_game, _ => _.Current is SecondPlayerTurnPerform).Current as SecondPlayerTurnPerform;
		}

		// IProvider<NotInFocus>
		NotInFocus IProvider<NotInFocus>.Get()
		{
			return Array.Find(_focus, _ => _.Current is NotInFocus).Current as NotInFocus;
		}

		// IProvider<InFocus>
		SelectorController IProvider<SelectorController>.Get()
		{
			return Array.Find(_focus, _ => _.Current is SelectorController).Current as SelectorController;
		}
	}
}
