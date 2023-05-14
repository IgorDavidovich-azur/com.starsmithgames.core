namespace StarSmithGames.Core
{
	public interface IAttribute : IValue<float>, IModifiable<AttributeModifier>
	{
		string Output { get; }

		string LocalizationKey { get; }
	}
}