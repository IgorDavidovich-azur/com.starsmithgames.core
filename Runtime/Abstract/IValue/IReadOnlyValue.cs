namespace StarSmithGames.Core
{
	public interface IReadOnlyValue<T> : IObservableValue
	{
		T CurrentValue { get; }
	}
}