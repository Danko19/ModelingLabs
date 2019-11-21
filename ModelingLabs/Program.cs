using System;
using System.IO;
using System.Text;

namespace ModelingLabs
{
	class Program
	{
		static void Main(string[] args)
		{
			var result = RandomGeneratorExplorer.Explore();
			foreach (var csvFile in result.ToCsvFiles)
			{
				File.WriteAllBytes(csvFile.FileName, Encoding.GetEncoding(1251).GetBytes(csvFile.Content));
			}
		}
	}
}
