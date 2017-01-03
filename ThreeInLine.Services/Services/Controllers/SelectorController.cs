using ThreeInLine.Services.FSM;
using ThreeInLine.Services.Interaction;
using UnityEngine;

namespace ThreeInLine.Services.Controllers
{
	public abstract class SelectorController : MonoBehaviourExtended, IState
	{
		private const float TRANSITION_SPEED_F = 2f;
		private const float IDLE_SPEED_F = 2f;
		
		private IFocusing _focusing;
		private int _handle;
		private Material _material;
		private float _phase;
		private float _innerPhase;
		private int _currentIndex;

		protected override void OnAwoke()
		{
			base.OnAwoke();

			Container.Container.CompositionRoot.RegisterInstance<SelectorController>(this);
			_focusing = Container.Container.CompositionRoot.Resolve<IFocusing>();
			_material = Renderer.material; // copy
			_handle = Shader.PropertyToID("_Phase");
		}

		public bool Rising()
		{
			_currentIndex = _focusing.InFocusIndex;
			Transform.position = _focusing.InFocus.Position;
			_phase += Time.smoothDeltaTime * TRANSITION_SPEED_F;
			_phase = Mathf.Clamp01(_phase);
			_material.SetFloat(_handle, _phase);
			return !Mathf.Approximately(_phase, 1f);
		}

		public bool Idle()
		{
			_innerPhase += Time.smoothDeltaTime * IDLE_SPEED_F;
			_innerPhase = _innerPhase % (Mathf.PI * 2f);
			_phase = 1f - Mathf.Sign(_innerPhase) * .3f;
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