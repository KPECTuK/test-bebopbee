using ThreeInLine.Services.Controllers;

namespace Assets.Scripts
{
	public class PieceControllerComponent : PieceController
	{
#if UNITY_EDITOR
		public void SetIndex(int index)
		{
			Index = index;
		}
#endif
	}
}
