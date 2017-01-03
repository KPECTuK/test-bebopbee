using ThreeInLine.Services.FSM;
using UnityEngine;

namespace ThreeInLine.Services.Controllers
{
	public abstract class PieceController : MonoBehaviourExtended, IPieceController, IState
	{
		[SerializeField]
		private int _index;
		public int Index { get { return _index; } protected set { _index = value; } }

		public Vector3 Position => Transform.position;

		public bool Rising()
		{
			throw new System.NotImplementedException();
		}

		public bool Idle()
		{
			throw new System.NotImplementedException();
		}

		public bool Fading()
		{
			throw new System.NotImplementedException();
		}
	}
}
