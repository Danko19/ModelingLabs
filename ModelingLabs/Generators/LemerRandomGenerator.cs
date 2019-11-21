using System;
using System.Collections.Generic;
using System.Linq;

namespace ModelingLabs.Generators
{
	public class LemerRandomGenerator : IRandomGenerator
	{
		private const int c = 65537;
		private const int limiter = 1_000_000;
		private const int n = 3;
		private long a;

		public LemerRandomGenerator(int a)
		{
			this.a = a % limiter;
		}

		public LemerRandomGenerator()
		{
			a = DateTime.Now.Ticks % limiter;
		}

		public string Name => "Генератор Лемера";

		public IEnumerable<int> GenerateRow(int min, int max, int count)
		{
			var diff = max - min + 1;
			if (diff <= 1)
				throw new ArgumentException($"Parameter max={max} should be bigger than min={min}");

			for (var i = 0; i < count; i++)
			{
				Update();
				yield return (int) RandomHelper.Normalize(a, 0, 1048575, min, max);
			}
		}

		private void Update()
		{
			var b1 = (a * a)
				.ToDecimalDigits()
				.Take(2 * n)
				.FromDecimalDigits();
			var b2 = (b1 * c)
				.ToDecimalDigits()
				.Skip(2 * n)
				.Take(2 * n)
				.FromDecimalDigits();
			var b3 = (b2 * b2)
				.ToDecimalDigits()
				.Skip(2 * n)
				.Take(2 * n)
				.FromDecimalDigits();
			var b4 = (b3 * c)
				.ToDecimalDigits()
				.Take(2 * n)
				.FromDecimalDigits();

			a = b1 ^ b2 ^ b3 ^ b4;
		}
	}
}