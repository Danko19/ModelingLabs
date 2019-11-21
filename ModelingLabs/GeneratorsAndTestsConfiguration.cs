using ModelingLabs.Generators;
using ModelingLabs.Tests;

namespace ModelingLabs
{
	public static class GeneratorsAndTestsConfiguration
	{
		public static readonly IRandomGenerator[] ExistingGenerators =
		{
			new DefaultRandomGenerator(17),
			new CryptoRandomGenerator(),
			new SequenceMethodRandomGenerator(100011),
			new LemerRandomGenerator(761347),
			new GPSSRandomGenerator(2131),
			new ComplexRandomGenerator(
				new SequenceMethodRandomGenerator(100011),
				new LemerRandomGenerator(761347),
				new GPSSRandomGenerator(2131))
		};

		public static readonly EmpiricalTest[] ExistingTests =
		{
			new UniformTest(0, 9, 100),
			new SeriesTest(0, 1, 2000),
			new IntervalTest(0, 9, 10000, 500),
			new PokerTest(0, 9, 100)
		};
	}
}