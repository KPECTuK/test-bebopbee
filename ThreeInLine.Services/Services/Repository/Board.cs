using System;
using ThreeInLine.Services.Container;

namespace ThreeInLine.Services.Repository
{
	internal class Board : IBoard
	{
		// mapping
		// 0, 1, 2,
		// 3, 4, 5,
		// 6, 7, 8

		// two players only

		public const int SIDE_SIZE_I = 3;
		public const int TOTAL_PIECES_AVALEABLE_I = 6;

		private readonly IContainer _container;
		private readonly int[] _cells = new int[SIDE_SIZE_I * SIDE_SIZE_I];
		private readonly Line[] _lines = new Line[8];
		private int _unusedPieces = TOTAL_PIECES_AVALEABLE_I;

		private class Line : ILine
		{
			private readonly Board _board;
			private readonly int[] _indices = new int[SIDE_SIZE_I];

			public int Length => _indices.Length;

			public int this[int index] => _board._cells[_indices[index]];

			internal Line(Board board, int[] indices)
			{
				_board = board;
				Array.Copy(indices, _indices, _indices.Length);
			}

			public bool IsComplete()
			{
				var sum = 0;
				// ReSharper disable once LoopCanBeConvertedToQuery, ForCanBeConvertedToForeach
				for(var index = 0; index < _indices.Length; index++)
					sum += _board._cells[_indices[index]];
				return sum == _indices.Length || sum == -_indices.Length;
			}
		}

		public Board(IContainer container)
		{
			_container = container;

			// horizontal
			_lines[0] = new Line(this, new[] { new IntVector(0, 0).ToOffset(), new IntVector(1, 0).ToOffset(), new IntVector(2, 0).ToOffset() });
			_lines[1] = new Line(this, new[] { new IntVector(0, 1).ToOffset(), new IntVector(1, 1).ToOffset(), new IntVector(2, 1).ToOffset() });
			_lines[2] = new Line(this, new[] { new IntVector(0, 2).ToOffset(), new IntVector(1, 2).ToOffset(), new IntVector(2, 2).ToOffset() });
			// vertical
			_lines[3] = new Line(this, new[] { new IntVector(0, 0).ToOffset(), new IntVector(0, 1).ToOffset(), new IntVector(0, 2).ToOffset() });
			_lines[4] = new Line(this, new[] { new IntVector(1, 0).ToOffset(), new IntVector(1, 1).ToOffset(), new IntVector(1, 2).ToOffset() });
			_lines[5] = new Line(this, new[] { new IntVector(2, 0).ToOffset(), new IntVector(2, 1).ToOffset(), new IntVector(2, 2).ToOffset() });
			// rest
			_lines[6] = new Line(this, new[] { new IntVector(0, 0).ToOffset(), new IntVector(1, 1).ToOffset(), new IntVector(2, 2).ToOffset() });
			_lines[7] = new Line(this, new[] { new IntVector(2, 0).ToOffset(), new IntVector(1, 1).ToOffset(), new IntVector(0, 2).ToOffset() });
		}

		public bool Turn(int player, IntVector from, IntVector to)
		{
			// check board cell if available
			if(!to.IsValid() || _cells[to.ToOffset()] != 0)
				return false;
			// remove piece from the board, a bag or exit without turn if there is no piece reachable for the player
			if(@from.IsValid() && _cells[@from.ToOffset()] == player)
				_cells[@from.ToOffset()] = 0;
			else if(_unusedPieces > 0)
				_unusedPieces -= 1;
			else
				return false;
			// set piece to the board
			_cells[to.ToOffset()] = player;
			return true;
		}

		public ILine GetLineComplete()
		{
			return Array.Find(_lines, _ => _.IsComplete());
		}
	}
}
