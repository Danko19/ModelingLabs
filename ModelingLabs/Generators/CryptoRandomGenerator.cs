using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace ModelingLabs.Generators
{
	public class CryptoRandomGenerator : IRandomGenerator
	{
		private readonly RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();

		public CryptoRandomGenerator(int fakeSeed) { }
		public CryptoRandomGenerator() { }

		public string Name => "Слуйчаный генератор C#";

		public IEnumerable<int> GenerateRow(int min, int max, int count)
		{
			var diff = max - min + 1;
			if (diff <= 1)
				throw new ArgumentException($"Parameter max={max} should be bigger than min={min}");
			var nextBytes = new byte[4];

			for (var i = 0; i < count; i++)
			{
				provider.GetBytes(nextBytes);
				var randomNumber = BitConverter.ToUInt32(nextBytes, 0);
				yield return (int) (randomNumber % diff) + min;
			}
		}
	}
}