using System;
using System.Collections.Generic;

namespace ModelingLabs.Generators
{
	public class DefaultRandomGenerator : IRandomGenerator
	{
		private readonly Random random;

		public DefaultRandomGenerator(int seed)
		{
			random = new Random(seed);
		}

		public DefaultRandomGenerator()
		{
			random = new Random();
		}

		public string Name => "Псевдослуйчаный генератор C#";

		public IEnumerable<int> GenerateRow(int min, int max, int count)
		{
			for (var i = 0; i < count; i++)
				yield return random.Next(min, max + 1);
		}
	}
}