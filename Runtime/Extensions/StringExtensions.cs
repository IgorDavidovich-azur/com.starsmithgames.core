namespace StarSmithGames.Core
{
	public static class StringExtensions
	{
		/// <summary>
		/// Checks if a string is empty.
		/// </summary>
		public static bool IsEmpty(this string s)
		{
			return string.IsNullOrEmpty(s) || string.IsNullOrWhiteSpace(s);
		}
	}
}