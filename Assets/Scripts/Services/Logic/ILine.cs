using System.Collections.Generic;

namespace ThreeInLine.Services.Logic
{
	public interface ILine : IEnumerable<int>
	{
		int this[int index] { get; }
		int Length { get; }
	}
}