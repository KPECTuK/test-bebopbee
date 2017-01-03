using UnityEngine;

namespace ThreeInLine.Services.Logic
{
	public struct IntVector
	{
		public readonly int X;
		public readonly int Y;

		public IntVector(int horizontal, int vertical)
		{
			X = horizontal;
			Y = vertical;
		}

		public IntVector(int offset)
		{
			X = offset % Board.SIDE_SIZE_I;
			Y = offset / Board.SIDE_SIZE_I;
		}

		public bool IsValid()
		{
			return
				Mathf.Clamp(X, 0, Board.SIDE_SIZE_I - 1) == X &&
				Mathf.Clamp(Y, 0, Board.SIDE_SIZE_I - 1) == Y;
		}

		public int ToOffset()
		{
			return Y * Board.SIDE_SIZE_I + X;
		}
	}
}