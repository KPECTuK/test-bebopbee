using System.Linq;
using ThreeInLine.Services.FSM;
using ThreeInLine.Services.Interaction;
using ThreeInLine.Services.Logic;
using UnityEngine;

namespace ThreeInLine.Services.Controllers
{
	public abstract class BoardInputController : MonoBehaviourExtended
	{
		private IFocusing _focusing;
		private IMachine _machine;
		private CameraController _cameraController;
		private bool _isDragging;
		private Vector3 _previousPosition;
		
		private readonly IPieceController[] _pieces = new IPieceController[Board.SIDE_SIZE_I * Board.SIDE_SIZE_I];

		protected override void OnAwoke()
		{
			base.OnAwoke();

			Container.Container.CompositionRoot.RegisterInstance<BoardInputController>(this);
			_focusing = Container.Container.CompositionRoot.Resolve<IFocusing>();
			_machine = Container.Container.CompositionRoot.Resolve<IMachine>();

			var pieces = Transform.GetComponentsInChildren<IPieceController>(true);
			for(var index = 0; index < _pieces.Length; index++)
				_pieces[index] = pieces.FirstOrDefault(_ => _.Index == index);
		}

		internal IPieceController GetPieceByIndex(int index)
		{
			return
				Mathf.Clamp(index, 0, _pieces.Length - 1) == index
					? _pieces[index]
					: null;
		}

		// ReSharper disable once UnusedMember.Local
		private void Update()
		{
			_cameraController = _cameraController ?? (_cameraController = Container.Container.CompositionRoot.Resolve<CameraController>());
			if(_cameraController == null)
				return;

			// read user input (the order is important here)
			Ray ray;
			_isDragging = Input.mousePresent
				? IsDragging(_cameraController.CurrentViewCamera, out ray)
				: IsSwiping(_cameraController.CurrentViewCamera, out ray);
			_focusing.MaintainSwipe(ray, _isDragging);
			if(!_isDragging)
			{
				var isAction = Input.mousePresent
					? IsClick(_cameraController.CurrentViewCamera, out ray)
					: IsTouch(_cameraController.CurrentViewCamera, out ray);
				_focusing.MaintainClick(ray, isAction);
			}
			// calculate game state
			_machine.MoveNext();
		}

		private bool IsClick(Camera camera, out Ray ray)
		{
			ray = camera.ScreenPointToRay(Input.mousePosition);
			return Input.GetMouseButtonDown(0);
		}
		
		private bool IsDragging(Camera camera, out Ray ray)
		{
			ray = camera.ScreenPointToRay(Input.mousePosition);
			var mouseDeltaPosition = Input.mousePosition - _previousPosition;
			_previousPosition = Input.mousePosition;
			return Input.GetMouseButton(0) && (_isDragging || mouseDeltaPosition.sqrMagnitude > 0f);
		}

		private bool IsTouch(Camera camera, out Ray ray)
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

		private bool IsSwiping(Camera camera, out Ray ray)
		{
			var isTouch =
				Input.touchCount == 0 &&
				Input.touches.Length > 0;
			ray =
				isTouch
					? new Ray() :
					camera.ScreenPointToRay(Input.touches[0].position);
			return isTouch && Input.touches[0].phase == TouchPhase.Moved;
		}
	}
}