using ThreeInLine.Services.Container;
using UnityEngine;

namespace ThreeInLine.Services
{
	public class MonoBehaviourExtended : MonoBehaviour
	{
		// component cache
		// cant use ?? with Unity types

		// ReSharper disable ConvertConditionalTernaryToNullCoalescing
		private Transform _transform;
		public Transform Transform { get { return _transform == null ? (_transform = transform) : _transform; } }
		private Renderer _renderer;
		// more complex routine
		public Renderer Renderer {get { return _renderer == null ? (_renderer = GetComponent<Renderer>()) : _renderer; } }
		// ReSharper restore ConvertConditionalTernaryToNullCoalescing

		protected IContainer CompositionRoot {get { return Container.Container.CompositionRoot; } }

		// ReSharper disable once UnusedMember.Local
		private void Awake()
		{
			OnAwoke();
		}

		protected virtual void OnAwoke() { }
	}
}