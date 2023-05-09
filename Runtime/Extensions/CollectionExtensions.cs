using System;
using System.Collections.Generic;
using System.Linq;

namespace StarSmithGames.Core
{
	public static class CollectionExtensions
	{
		public static T RandomItem<T>(this IList<T> list, int from = 0, int to = -1)
		{
			if (list.Count == 0) return default;
			return list[UnityEngine.Random.Range(from, to == -1 ? list.Count : to)];
		}

		public static T RandomItem<T>(this IList<T> list, int from = 0, int to = -1, params T[] except)
		{
			if (list.Count == 0) return default;

			var check = list.Except(except).ToList();
			return check[UnityEngine.Random.Range(from, to == -1 ? check.Count : to)];
		}

		public static List<T> Shuffle<T>(this IList<T> list)
		{
			Random rnd = new Random();
			var copy = new List<T>(list);
			int n = copy.Count;
			while (n > 1)
			{
				n--;
				int k = rnd.Next(n + 1);
				T value = copy[k];
				copy[k] = copy[n];
				copy[n] = value;
			}

			return copy;
		}
	}
}