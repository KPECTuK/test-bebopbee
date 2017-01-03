using ThreeInLine.Services.FSM;
using ThreeInLine.Services.Interaction;
using UnityEngine;

namespace ThreeInLine.Services.Controllers
{
	public abstract class BoardInputController : MonoBehaviourExtended
	{
		private IFocusing _focusing;
		private IMachine _machine;
		private CameraController _cameraController;

		protected override void OnAwoke()
		{
			base.OnAwoke();

			Container.Container.CompositionRoot.RegisterInstance<BoardInputController>(this);
			_focusing = Container.Container.CompositionRoot.Resolve<IFocusing>();
			_machine = Container.Container.CompositionRoot.Resolve<IMachine>();
		}

		// ReSharper disable once UnusedMember.Local
		private void Update()
		{
			_cameraController = _cameraController ?? (_cameraController = Container.Container.CompositionRoot.Resolve<CameraController>());
			if(_cameraController == null)
				return;

			Ray ray;
			var isIntent = Input.mousePresent
				? ReadMouseInput(_cameraController.CurrentViewCamera, out ray)
				: ReadTouchInput(_cameraController.CurrentViewCamera, out ray);
			_focusing.Scan(ray, isIntent);
			_machine.MoveNext();
		}

		private bool ReadMouseInput(Camera camera, out Ray ray)
		{
			ray = camera.ScreenPointToRay(Input.mousePosition);
			return Input.GetMouseButtonUp(0);
		}

		private bool ReadTouchInput(Camera camera, out Ray ray)
		{
			var isTouch =
				Input.touchCount == 0 &&
				Input.touches.Length > 0;
			ray =
				isTouch
					? new Ray() :
					camera.ScreenPointToRay(Input.touches[0].position);
			return isTouch;
		}
	}
}