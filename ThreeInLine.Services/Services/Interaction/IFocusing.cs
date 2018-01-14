using ThreeInLine.Services.Controllers;
using UnityEngine;

namespace ThreeInLine.Services.Interaction
{
	public interface IFocusing
	{
		int InFocusIndex { get; }
		bool WasDoubleClick { get; }
		bool WasSwipe { get; }
		IPieceController InFocus { get; }
		IPieceController SwipedTo { get; }
		void MaintainClick(Ray ray, bool isAction);
		void MaintainSwipe(Ray ray, bool isAction);
	}
}