namespace StarSmithGames.Core
{
	public interface IBounded<T>
	{
		T MinValue { get; }
		T MaxValue { get; }
	}

	public interface IBar : IBounded<float>
	{
		float PercentValue { get; }
	}
}