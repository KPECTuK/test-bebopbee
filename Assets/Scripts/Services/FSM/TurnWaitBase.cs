using System;
using System.Linq;
using ThreeInLine.Services.Container;
using ThreeInLine.Services.Controllers;
using ThreeInLine.Services.Interaction;
using ThreeInLine.Services.Logging;
using ThreeInLine.Services.Logic;
using UnityEngine;

namespace ThreeInLine.Services.FSM
{
	internal abstract class TurnWaitBase : StateTemplate
	{
		private const float PULSE_SPEED_F = 2f;
		private const float PULSE_AMP_F = .2f;

		protected abstract int PlayerIndex { get; }
		protected abstract TurnPerformBase PerformingState { get; }

		private readonly IFocusing _focusing;
		private readonly IBoard _board;
		private BoardInputController _boardController;
		private ILine _completeLine;
		private IPieceController[] _controllers;
		private float _phase;

		protected TurnWaitBase(IContainer container) : base(container)
		{
			_focusing = Container.Resolve<IFocusing>();
			_board = Container.Resolve<IBoard>();
		}

		public override void ResetState()
		{
			// check for win here
			// should be an another state, but machine turns into more complex because of that
			// branching should be implemented to achieve this
			// so it is a wining stub
			_completeLine = _board.GetLineComplete();
		}

		public override bool Rising()
		{
			return false;
		}

		public override bool Idle()
		{
			// hate ifs..
			if(_completeLine != null)
			{
				_boardController = _boardController ?? (_boardController = Container.Resolve<BoardInputController>());
				_controllers = _controllers ?? (_controllers = _completeLine.Select(_ => _boardController.GetPieceByIndex(_)).ToArray());
				_phase += Time.smoothDeltaTime * PULSE_SPEED_F;
				_phase = _phase % SelectorController.DOUBLE_PI_F;
				var size = 1f - Mathf.Sin(_phase) * PULSE_AMP_F - PULSE_AMP_F * .5f;
				Array.ForEach(_controllers, _ => _.SetSize(size));

				return true;
			}

			//// swipe to
			if(_focusing.WasSwipe && _focusing.InFocus != null && _board.Turn(PlayerIndex, _focusing.InFocus.Index, _focusing.SwipedTo.Index))
			{
				this.Log(Level.Info, "swiped: "+ _focusing.InFocus.Index + " -> " +_focusing.SwipedTo.Index);
				PerformingState.FromIndex = _focusing.InFocus.Index;
				PerformingState.ToIndex = _focusing.SwipedTo.Index;
				return false;
			}

			// piece is not on the board
			if(_focusing.WasDoubleClick)
			{
				if(!_board.Turn(PlayerIndex, -1, _focusing.InFocus == null ? -1 : _focusing.InFocus.Index))
					return true;

				PerformingState.FromIndex = -1;
				PerformingState.ToIndex = _focusing.InFocus == null ? -1 : _focusing.InFocus.Index;
				return false;
			}

			return true;
		}

		public override bool Fading()
		{
			return false;
		}
	}
}