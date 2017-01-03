using UnityEngine;

namespace ThreeInLine.Services.Controllers
{
	public abstract class PieceController : MonoBehaviourExtended, IPieceController
	{
		[SerializeField] private int _index;
		public int Index { get { return _index; } protected set { _index = value; } }

		public Vector3 Position => Transform.position;

		private Material _material;
		private int _sizeHandle;
		private int _stateHandle;

		protected override void OnAwoke()
		{
			base.OnAwoke();

			_material = Renderer.material; // copy
			_sizeHandle = Shader.PropertyToID("_Size");
			_stateHandle = Shader.PropertyToID("_State");
			//
			_material.SetFloat(_stateHandle, 0f);
			_material.SetFloat(_sizeHandle, 0f);
		}

		void IPieceController.SetState(int player)
		{
			_material.SetFloat(_stateHandle, Mathf.Clamp(player, -.5f, .5f));
		}

		void IPieceController.SetSize(float normalized)
		{
			_material.SetFloat(_sizeHandle, Mathf.Clamp01(normalized));
		}
	}
}
