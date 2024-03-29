using System;
using System.Collections.Generic;

namespace StarSmithGames.Core
{
	public interface IModifiable<M> where M : Modifier<float>
	{
		event Action onModifiersChanged;

		float TotalValue { get; }
		float ModifyAddValue { get; }
		float ModifyPercentValue { get; }

		List<M> Modifiers { get; }

		bool AddModifier(M modifier);
		bool RemoveModifier(M modifier);

		bool Contains(M modifier);
	}
}