namespace ModelingLabs
{
	public class RandomHelper
	{
		public static long Normalize(long value, long min, long max, long destinationMin, long destinationMax)
		{
			var diff = max - min + 1;
			var destinationDiff = destinationMax - destinationMin + 1;
			return (long) ((double) destinationDiff / diff * (value - min)) + destinationMin;
		}
	}
}