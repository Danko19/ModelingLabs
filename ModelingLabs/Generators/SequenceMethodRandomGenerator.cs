using System;
using System.Collections.Generic;
using System.Linq;

namespace ModelingLabs.Generators
{
	public class SequenceMethodRandomGenerator : IRandomGenerator
	{
		private const int limiter = 1_000_000;
		private const int n = 3;
		private long x;

		public SequenceMethodRandomGenerator(int x)
		{
			this.x = x % limiter;
		}

		public SequenceMethodRandomGenerator()
		{
			x = DateTime.Now.Ticks % limiter;
		}

		public string Name => "Генератор методом усечения";

		public IEnumerable<int> GenerateRow(int min, int max, int count)
		{
			var diff = max - min + 1;
			if (diff <= 1)
				throw new ArgumentException($"Parameter max={max} should be bigger than min={min}");

			for (var i = 0; i < count; i++)
			{
				Update();
				yield return (int) RandomHelper.Normalize(x, 0, limiter - 1, min, max);
			}
		}

		private void Update()
		{
			x = (x * x)
				.ToDecimalDigits()
				.Skip(n)
				.Take(2 * n)
				.FromDecimalDigits();
		}
	}
}