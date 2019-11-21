using System.Linq;
using ModelingLabs.Generators;

namespace ModelingLabs.Tests
{
	public class UniformTest : EmpiricalTest
	{
		public UniformTest(int min, int max, int count) : base(min, max, count)
		{
		}

		public override string Name => "Критерий равномерности";

		public override string Check(IRandomGenerator randomGenerator)
		{
			var randomRow = GetNextRow(randomGenerator);
			var ys = new int[Diff];
			foreach (var i in randomRow)
				ys[i]++;

			var theoreticalY = (double) randomRow.Length / Diff;

			var v = ys.Sum(y => (y - theoreticalY) * (y - theoreticalY) / (theoreticalY));

			return FormatResult(v, Diff);
		}
	}
}