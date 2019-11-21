using System.Collections.Generic;

namespace ModelingLabs.Generators
{
	public interface IRandomGenerator
	{
		IEnumerable<int> GenerateRow(int min, int max, int count);
		string Name { get; }
	}
}