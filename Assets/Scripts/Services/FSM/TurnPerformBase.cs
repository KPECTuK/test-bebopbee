using ThreeInLine.Services.Container;
using ThreeInLine.Services.Controllers;
using UnityEngine;

namespace ThreeInLine.Services.FSM
{
	internal abstract class TurnPerformBase : StateTemplate
	{
		private const float TRANSITION_SPEED_F = 2f;

		internal int FromIndex { private get; set; }
		internal int ToIndex { private get; set; }

		protected abstract int PlayerIndex { get; }

		private BoardInputController _boardController;
		private IPieceController _fromController;
		private IPieceController _toController;
		private float _phase;

		protected TurnPerformBase(IContainer container) : base(container) { }

		public override void ResetState()
		{
			_boardController = _boardController ?? Container.Resolve<BoardInputController>();
			// might be zeroed, but not necessary here
			_fromController = FromIndex == -1 ? null : _boardController.GetPieceByIndex(FromIndex);
			_toController = ToIndex == -1 ? null : _boardController.GetPieceByIndex(ToIndex);
			_phase = 1f;
		}

		public override bool Rising()
		{
			if(_fromController == null)
				return false;

			_phase -= Time.smoothDeltaTime * TRANSITION_SPEED_F;
			_phase = Mathf.Clamp01(_phase);
			_fromController.SetSize(_phase);

			return !Mathf.Approximately(_phase, 0f);
		}

		public override bool Idle()
		{
			// may start vfx here

			_phase = 0f;
			_toController.SetState(PlayerIndex);
			return false;
		}

		public override bool Fading()
		{
			if(_toController == null)
				return false;

			_phase += Time.smoothDeltaTime * TRANSITION_SPEED_F;
			_phase = Mathf.Clamp01(_phase);
			_toController.SetSize(_phase);

			return !Mathf.Approximately(_phase, 1f);
		}
	}
}