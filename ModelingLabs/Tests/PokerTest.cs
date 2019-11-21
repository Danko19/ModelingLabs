using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using ModelingLabs.Generators;
using MoreLinq.Extensions;

namespace ModelingLabs.Tests
{
	public class PokerTest : EmpiricalTest
	{
		private readonly double[] ps = new double[7];

		public PokerTest(int min, int max, int c) : base(min, max, c)
		{
			var d = max - min + 1;
			ps[0] = (double)(d - 1) * (d - 2) * (d - 3) * (d - 4) / Math.Pow(d, 4);
			ps[1] = 10.0 * (d - 1) * (d - 2) * (d - 3) / Math.Pow(d, 4);
			ps[2] = 15.0 * (d - 1) * (d - 2) / Math.Pow(d, 4);
			ps[3] = 10.0 * (d - 1) * (d - 2) / Math.Pow(d, 4);
			ps[4] = 10.0 * (d - 1) / Math.Pow(d, 4);
			ps[5] = 5.0 * (d - 1) / Math.Pow(d, 4);
			ps[6] = 1.0 / Math.Pow(d, 4);
		}

		public override string Name => "Покер-критерий";

		public override string Check(IRandomGenerator randomGenerator)
		{
			var randomRow = GetNextRow(randomGenerator);
			var combos = new int[7];

			foreach (var group in randomRow.Batch(5))
			{
				var stats = GetStats(group);
				var rate = RateCombo(stats);
				combos[rate]++;
			}

			var n = Count / 7.0;

			var v = combos
				.Select((val, index) => (val - n * ps[index]) * (val - n * ps[index]) / (n * ps[index]))
				.Sum();

			return FormatResult(v, 6);
		}

		private Dictionary<int, int> GetStats(IEnumerable<int> source)
		{
			var result = new Dictionary<int, int>();

			foreach (var e in source)
			{
				if (!result.ContainsKey(e))
					result[e] = 1;
				else result[e]++;
			}

			return result;
		}

		private int RateCombo(Dictionary<int, int> stats)
		{
			if (stats.Count == 1 && stats.Single().Value == 5)
				return 6;

			if (stats.Count == 2 && stats.Any(x => x.Value == 4))
				return 5;

			if (stats.Count == 2 && stats.Any(x => x.Value == 3))
				return 4;

			if (stats.Count == 3 && stats.Any(x => x.Value == 3))
				return 3;

			if (stats.Count == 3 && stats.Any(x => x.Value == 2))
				return 2;

			if (stats.Count == 4 && stats.Any(x => x.Value == 2))
				return 1;

			if (stats.Count == 5 && stats.All(x => x.Value == 1))
				return 0;

			throw new ArgumentException();
		}
	}
}