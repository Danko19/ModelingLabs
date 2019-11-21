using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelingLabs
{
	public class GeneratorResult
	{
		private readonly List<Dictionary<string,string>> results = new List<Dictionary<string, string>>();
		private readonly string generatorName;

		public GeneratorResult(string generatorName)
		{
			this.generatorName = generatorName;
		}

		public void RegisterResult(int attempt, string testName, string testResult)
		{
			while (results.Count < attempt)
				results.Add(new Dictionary<string, string>());

			results[attempt - 1][testName] = testResult;
		}


		public SingleFile ToCsv()
		{
			var tests = results.SelectMany(x => x.Keys).Distinct().ToArray();
			if (!tests.Any())
			{
				return null;
			}
			var sb = new StringBuilder();
			sb.Append("№;");
			sb.Append(string.Join(";", tests));
			sb.AppendLine();
			var i = 0;
			foreach (var kvp in results)
			{
				sb.Append(++i);
				foreach (var test in tests)
				{
					sb.Append(";");
					if (kvp.TryGetValue(test, out var res))
					{
						sb.Append(res);
					}
				}

				sb.AppendLine();
			}

			return new SingleFile($"{generatorName}.csv", sb.ToString());
		}
	}
}