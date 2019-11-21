using System;
using System.Globalization;
using System.Linq;
using ModelingLabs.Generators;

namespace ModelingLabs.Tests
{
	public abstract class EmpiricalTest
	{
		protected readonly int Min;
		protected readonly int Max;
		protected readonly int Count;

		protected EmpiricalTest(int min, int max, int count)
		{
			Min = min;
			Max = max;
			Count = count;
		}

		public abstract string Check(IRandomGenerator randomGenerator);
		public abstract string Name { get; }

		protected int[] GetNextRow(IRandomGenerator randomGenerator)
		{
			return randomGenerator.GenerateRow(Min, Max, Count).ToArray();
		}

		protected int Diff => Max - Min + 1;

		protected static string FormatResult(double result, int k)
		{
			var percentage = XIStatConsts.GetPercentage(k, result);
			var ok = percentage < 75 && percentage > 25;
			return $"{GetRounded(result, 3)} ({GetRounded(percentage, 1)}% {(ok ? "OK" : "X")})";
		}

		private static string GetRounded(double number, int digits)
		{
			return Math.Round(number, digits).ToString(CultureInfo.InvariantCulture);
		}
	}
}