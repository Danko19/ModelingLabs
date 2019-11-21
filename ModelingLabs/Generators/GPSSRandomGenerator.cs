using System;
using System.Collections.Generic;
using System.Linq;

namespace ModelingLabs.Generators
{
	public class GPSSRandomGenerator : IRandomGenerator
	{
		private const int limiter = 10000;
		private const int n = 4;
		private const int c = 5167;
		private int m;
		private int x;

		public GPSSRandomGenerator(int m)
		{
			this.m = GetUneven(m) % limiter;
		}

		public GPSSRandomGenerator()
		{
			m = GetUneven((int)(DateTime.Now.Ticks % int.MaxValue) - 1) % limiter;
		}

		public string Name => "Генератор из языка GPSS";

		public IEnumerable<int> GenerateRow(int min, int max, int count)
		{
			var diff = max - min + 1;
			if (diff <= 1)
				throw new ArgumentException($"Parameter max={max} should be bigger than min={min}");

			for (var i = 0; i < count; i++)
			{
				Update();
				yield return (int)RandomHelper.Normalize(x, 0, limiter, min, max);
			}
		}

		private void Update()
		{
			var digits = ((long)m * c).ToDecimalDigits().ToArray();
			m = (int)digits.Take(4).FromDecimalDigits();
			x = (int) digits.Skip(2).Take(4).FromDecimalDigits();
		}

		private int GetUneven(int source)
		{
			return source % 2 == 1 ? source : source + 1;
		}
	}
}