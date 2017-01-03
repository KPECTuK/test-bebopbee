using ThreeInLine.Services.Controllers;
using UnityEngine;

namespace ThreeInLine.Services.Interaction
{
	public interface IFocusing
	{
		int InFocusIndex { get; }
		IPieceController InFocus { get; }
		void Scan(Ray ray, bool isIntent);
	}
}