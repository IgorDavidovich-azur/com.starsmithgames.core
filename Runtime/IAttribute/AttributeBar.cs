using System;
using UnityEngine;

namespace StarSmithGames.Core
{
	public abstract partial class AttributeBar : Attribute, IBar
	{
		public override string Output => $"{Math.Round(CurrentValue)} / {Math.Round(TotalValue)}";

		public override float CurrentValue
		{
			get => currentValue;
			set
			{
				base.CurrentValue = Mathf.Clamp(value, MinValue, TotalValue);
			}
		}

		public virtual float MaxValue
		{
			get => maxValue;
			set
			{
				maxValue = value;
				base.CurrentValue = Mathf.Clamp(currentValue, MinValue, TotalValue);
			}
		}
		protected float maxValue;

		public virtual float MinValue { get; protected set; }

		public float PercentValue => CurrentValue / TotalValue;

		protected AttributeBar(float value, float min, float max) : base(value)
		{
			this.maxValue = max;
			this.MinValue = min;
			this.CurrentValue = value;
		}
	}

	//IModifiable Implementation
	public abstract partial class AttributeBar
	{
		public override float TotalValue => (MaxValue + ModifyAddValue) * (1f + (ModifyPercentValue / 100f));

		public override bool AddModifier(AttributeModifier modifier)
		{
			if (base.AddModifier(modifier))
			{
				CurrentValue = currentValue;//upd
				return true;
			}

			return false;
		}

		public override bool RemoveModifier(AttributeModifier modifier)
		{
			if (base.RemoveModifier(modifier))
			{
				CurrentValue = currentValue;//upd
				return true;
			}

			return false;
		}
	}
}