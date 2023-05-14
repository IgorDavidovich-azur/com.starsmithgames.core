namespace StarSmithGames.Core
{
	public interface IValue<T> : IReadOnlyValue<T>
	{
		new T CurrentValue { get; set; }
	}
}