namespace ModelingLabs
{
	public class SingleFile
	{
		public SingleFile(string fileName, string content)
		{
			FileName = fileName;
			Content = content;
		}

		public string FileName { get; }
		public string Content { get; }
	}
}