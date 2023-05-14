using System;
using System.Collections.Generic;

namespace StarSmithGames.Core
{
	public abstract partial class Attribute : IAttribute
	{
		public event Action onChanged;

		public virtual string Output => TotalValue.ToString();

		public virtual string LocalizationKey => "sheet.";

		public virtual float CurrentValue
		{
			get => currentValue;
			set
			{
				currentValue = value;
				onChanged?.Invoke();
			}
		}
		protected float currentValue;

		public Attribute(float currentValue)
		{
			this.currentValue = currentValue;

			Modifiers = new List<AttributeModifier>();
		}
	}

	//IModifiable Implementation
	public abstract partial class Attribute
	{
		public event Action onModifiersChanged;

		public virtual float TotalValue => (CurrentValue + ModifyAddValue) * (1f + (ModifyPercentValue / 100f));

		public virtual float ModifyAddValue
		{
			get
			{
				float value = 0;

				Modifiers.ForEach((modifier) =>
				{
					if (modifier is AddModifier)
					{
						value += modifier.CurrentValue;
					}
				});

				return value;
			}
		}

		public virtual float ModifyPercentValue
		{
			get
			{
				float value = 0;

				Modifiers.ForEach((modifier) =>
				{
					if (modifier is PercentModifier)
					{
						value += modifier.CurrentValue;
					}
				});

				return value;
			}
		}

		public List<AttributeModifier> Modifiers { get; }

		public virtual bool AddModifier(AttributeModifier modifier)
		{
			if (!Contains(modifier))
			{
				Modifiers.Add(modifier);

				onModifiersChanged?.Invoke();

				return true;
			}

			return false;
		}

		public virtual bool RemoveModifier(AttributeModifier modifier)
		{
			if (Contains(modifier))
			{
				Modifiers.Remove(modifier);

				onModifiersChanged?.Invoke();

				return true;
			}

			return false;
		}

		public bool Contains(AttributeModifier modifier) => Modifiers.Contains(modifier);
	}
}