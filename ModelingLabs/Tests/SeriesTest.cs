using System.Globalization;
using System.Linq;
using ModelingLabs.Generators;

namespace ModelingLabs.Tests
{
	public class SeriesTest : EmpiricalTest
	{
		public SeriesTest(int min, int max, int count) : base(min, max, count)
		{
		}
		public override string Name => "Критерий серий";

		public override string Check(IRandomGenerator randomGenerator)
		{
			var randomRow = GetNextRow(randomGenerator);
			var groupsCount = Diff * Diff;
			var stats = new int[groupsCount];
			var length = randomRow.Length % 2 == 0 ? randomRow.Length : randomRow.Length - 1;

			for (var i = 0; i < length; i += 2)
			{
				var item1 = randomRow[i];
				var item2 = randomRow[i + 1];
				var index = item1 * Diff + item2;
				stats[index]++;
			}

			var theoreticalPairsCount = (double) randomRow.Length / 2 / groupsCount;
			var result = stats
				.Select(y => y - theoreticalPairsCount)
				.Select(y => y * y)
				.Select(y => y / theoreticalPairsCount)
				.Sum();

			return FormatResult(result, groupsCount - 1);
		}
	}
}