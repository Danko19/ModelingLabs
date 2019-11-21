using System;
using System.Globalization;
using System.Linq;
using ModelingLabs.Generators;

namespace ModelingLabs.Tests
{
	public class IntervalTest : EmpiricalTest
	{
		private const int t = 15;
		private readonly int n;
		private readonly double median;
		private readonly double[] ps;

		public IntervalTest(int min, int max, int count, int n) : base(min, max, count)
		{
			this.n = n;
			median = ((double) max - min) / 2;

			var satisfying = (int) Math.Ceiling(median - min);
			var p = (double) satisfying / Diff;
			ps = new double[t + 1];

			for (var r = 0; r < t; r++)
			{
				ps[r] = p * Math.Pow((1 - p), r);
			}

			ps[t] = Math.Pow((1 - p), t);
		}

		public override string Name => "Критерий интервалов";

		public override string Check(IRandomGenerator randomGenerator)
		{
			var randomRow = GetNextRow(randomGenerator);

			var s = 0;
			var stats = new int[t + 1];
			var r = 0;

			foreach (var number in randomRow)
			{
				if (number >= median)
				{
					r++;
					continue;
				}

				stats[Math.Min(t, r)]++;
				s++;

				if (s >= n)
					break;

				r = 0;
			}

			var result = stats
				.Select((value, index) => (value - ps[index] * s) * (value - ps[index] * s) / (ps[index] * s))
				.Sum();

			return FormatResult(result, t);
		}
	}
}