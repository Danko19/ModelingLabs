using System.Collections.Generic;
using System.Linq;

namespace ModelingLabs.Generators
{
	public class ComplexRandomGenerator : IRandomGenerator
	{
		private readonly IRandomGenerator generator1;
		private readonly IRandomGenerator generator2;
		private readonly IRandomGenerator generator3;

		public ComplexRandomGenerator(IRandomGenerator generator1, IRandomGenerator generator2,
			IRandomGenerator generator3)
		{
			this.generator1 = generator1;
			this.generator2 = generator2;
			this.generator3 = generator3;
		}

		public IEnumerable<int> GenerateRow(int min, int max, int count)
		{
			var diff = max - min + 1;
			var row1 = generator1.GenerateRow(min, max, count).ToArray();
			var row2 = generator2.GenerateRow(min, max, count).ToArray();
			var row3 = generator3.GenerateRow(min, max, count).ToArray();
			for (var i = 0; i < count; i++)
			{
				yield return (row1[i] + row2[i] + row3[i]) % diff + min;
			}
		}

		public string Name => "Комбинированный генератор";
	}
}