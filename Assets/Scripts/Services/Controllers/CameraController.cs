using UnityEngine;

namespace ThreeInLine.Services.Controllers
{
	public abstract class CameraController : MonoBehaviourExtended
	{
		// it is not necessary for that class to be a component

		public Camera CurrentViewCamera { get; private set; }

		// ReSharper disable once UnusedMember.Local
		protected override void OnAwoke()
		{
			base.OnAwoke();

			CurrentViewCamera = Transform.GetComponentInChildren<Camera>(true);
			Container.Container.CompositionRoot.RegisterInstance<CameraController>(this);
		}
	}
}