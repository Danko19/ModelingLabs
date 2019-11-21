using static ModelingLabs.GeneratorsAndTestsConfiguration;

namespace ModelingLabs
{
	public static class RandomGeneratorExplorer
	{
		private const int attempts = 30;

		public static ExploreResult Explore()
		{
			var exploreResult = new ExploreResult();

			foreach (var generator in ExistingGenerators)
			{
				var generatorName = generator.Name;
				for (var attempt = 1; attempt <= attempts; attempt++)
				{
					foreach (var test in ExistingTests)
					{
						var testResult = test.Check(generator);
						exploreResult.Add(attempt, generatorName, test.Name, testResult);
					}
				}
			}

			return exploreResult;
		}

	}
}