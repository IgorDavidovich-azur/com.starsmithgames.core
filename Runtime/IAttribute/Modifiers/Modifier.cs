using System;

namespace StarSmithGames.Core
{
	public abstract class Modifier<T> : IReadOnlyValue<T>
	{
		public event Action onChanged;

		public T CurrentValue { get; }

		public Modifier(T value)
		{
			CurrentValue = value;
		}
	}

	public abstract class AttributeModifier : Modifier<float>
	{
		protected AttributeModifier(float value) : base(value) { }
	}
}