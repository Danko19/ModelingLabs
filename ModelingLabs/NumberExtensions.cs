using System;
using System.Collections.Generic;
using System.Linq;

namespace ModelingLabs
{
	public static class NumberExtensions
	{
		public static IEnumerable<byte> ToDecimalDigits(this long number, bool fromLowToHigh = true)
		{
			if (number < 0)
				throw new ArgumentException($"Parameter number={number} must be positive");

			var result = new List<byte>();
			while (number > 0)
			{
				result.Add((byte)(number % 10));
				number /= 10;
			}

			if (!fromLowToHigh)
				result.Reverse();

			return result;
		}

		public static long FromDecimalDigits(this IEnumerable<byte> digits, bool fromLowToHigh = true)
		{
			if (!fromLowToHigh)
				digits = digits.Reverse();

			var result = 0L;
			var weight = 1;

			foreach (var digit in digits)
			{
				result += digit * weight;
				weight *= 10;
			}

			return result;
		}

	}
}