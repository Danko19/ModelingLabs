using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluentAssertions;
using ModelingLabs;
using ModelingLabs.Generators;
using ModelingLabs.Tests;
using NUnit.Framework;

namespace ModelingLabsTests
{
	[TestFixture]
	[Parallelizable(ParallelScope.All)]
	public class RandomGeneratorsSimpleTests
	{
		private const int min = 0;
		private const int max = 9;
		private const int count = 1000;
		private const int repeats = 10;

		[Test]
		[TestCaseSource(nameof(TestCases))]
		public void SimpleTests(IRandomGenerator randomGenerator)
		{
			for (var i = 0; i < repeats; i++)
			{
				var randomRow = randomGenerator.GenerateRow(min, max, count).ToArray();
				randomRow.Length.Should().Be(count);
				randomRow.All(x => x >= min).Should().BeTrue();
				randomRow.All(x => x <= max).Should().BeTrue();
			}
		}

		[Test]
		public void Test()
		{
			//var lemerRandomGenerator = new GPSSRandomGenerator(3729);
			////var lemerRandomGenerator = new SequenceMethodRandomGenerator(100011);
			//var lemerRandomGenerator = new LemerRandomGenerator(100011);
			var lemerRandomGenerator = new CryptoRandomGenerator();
			//var uniformTest = new PokerTest(0, 9, 100);
			var uniformTest = new IntervalTest(0, 10, 10000, 500);
			for (var i = 0; i < 5; i++)
			{
				Console.WriteLine(uniformTest.Check(lemerRandomGenerator));
			}
		}

		[Test]
		[TestCase(50, 0, 99, 0, 9, 5)]
		[TestCase(51, 0, 99, 0, 9, 5)]
		[TestCase(1, 0, 99, 0, 9, 0)]
		[TestCase(53, 2, 101, 1, 10, 6)]
		public void RandomHelperTest(int value, int sourceMin, int sourceMax, int destMin, int destMax, int expectedValue)
		{
			var normalize = RandomHelper.Normalize(value, sourceMin, sourceMax, destMin, destMax);
			normalize.Should().Be(expectedValue);
		}

		private static IEnumerable<TestCaseData> TestCases()
		{
			return Assembly
				.Load("ModelingLabs")
				.GetTypes()
				.Where(type => typeof(IRandomGenerator).IsAssignableFrom(type))
				.Select(type => type.GetConstructor(new Type[0])?.Invoke(new object[0]) as IRandomGenerator)
				.Where(generator => generator != null)
				.Select(generator => new TestCaseData(generator) { TestName = generator.GetType().Name });
		}
	}
}