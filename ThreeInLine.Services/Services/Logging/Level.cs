using System;

namespace ThreeInLine.Services.Logging
{
	public class Level : IEquatable<Level>, IComparable<Level>
	{
		private readonly int _level;

		private Level(int level)
		{
			_level = level;
		}

		public static readonly Level Debug = new Level(500);
		public static readonly Level Info = new Level(400);
		public static readonly Level Notice = new Level(400);
		public static readonly Level Warn = new Level(200);
		public static readonly Level Error = new Level(100);

		public bool Equals(Level other)
		{
			return !ReferenceEquals(null, other) && other._level == _level;
		}

		public int CompareTo(Level other)
		{
			return ReferenceEquals(null, other) ? -1 : _level.CompareTo(other);
		}
	}
}