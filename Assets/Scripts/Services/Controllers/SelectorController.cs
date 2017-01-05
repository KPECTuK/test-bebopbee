using ThreeInLine.Services.FSM;
using ThreeInLine.Services.Interaction;
using UnityEngine;

namespace ThreeInLine.Services.Controllers
{
	public abstract class SelectorController : MonoBehaviourExtended, IState
	{
		//? probably its better to have separate focus indicator on each piece (speed)

		private const float TRANSITION_SPEED_F = 2f;
		private const float IDLE_SPEED_F = 2f;
		private const float PULSE_AMP_F = .2f;
		public const float DOUBLE_PI_F = Mathf.PI * 2f;
		
		private IFocusing _focusing;
		private Material _material;
		private int _handle;
		private float _phase;
		private float _innerPhase;
		private int _currentIndex;

		protected override void OnAwoke()
		{
			base.OnAwoke();

			Container.Container.CompositionRoot.RegisterInstance<SelectorController>(this);
			_focusing = Container.Container.CompositionRoot.Resolve<IFocusing>();
			_material = Renderer.material; // copy
			_handle = Shader.PropertyToID("_Size");
			_material.SetFloat(_handle, _phase);
		}

		public void ResetState()
		{
			_currentIndex = _focusing.InFocusIndex;
			_innerPhase = 0f;
			Transform.position = _focusing.InFocus.Position;
		}

		public bool Rising()
		{
			_phase += Time.smoothDeltaTime * TRANSITION_SPEED_F;
			_phase = Mathf.Clamp01(_phase);
			_material.SetFloat(_handle, _phase);
			return !Mathf.Approximately(_phase, 1f);
		}

		public bool Idle()
		{
			_innerPhase += Time.smoothDeltaTime * IDLE_SPEED_F;
			_innerPhase = _innerPhase % DOUBLE_PI_F;
			_phase = 1f - Mathf.Sin(_innerPhase) * PULSE_AMP_F - PULSE_AMP_F * .5f;
			_material.SetFloat(_handle, _phase);
			return _currentIndex == _focusing.InFocusIndex;
		}

		public bool Fading()
		{
			_phase -= Time.smoothDeltaTime * TRANSITION_SPEED_F;
			_phase = Mathf.Clamp01(_phase);
			_material.SetFloat(_handle, _phase);
			return !Mathf.Approximately(_phase, 0f);
		}
	}
}