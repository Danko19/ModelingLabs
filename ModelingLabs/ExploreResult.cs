using System.Collections.Generic;
using System.Linq;

namespace ModelingLabs
{
	public class ExploreResult
	{
		private readonly Dictionary<string, GeneratorResult> results = new Dictionary<string, GeneratorResult>();

		public void Add(int attempt, string generatorName, string testName, string testResult)
		{
			if (!results.ContainsKey(generatorName))
				results.Add(generatorName, new GeneratorResult(generatorName));

			results[generatorName].RegisterResult(attempt, testName, testResult);
		}

		public IEnumerable<SingleFile> ToCsvFiles => results.Values.Select(r => r.ToCsv());
	}
}