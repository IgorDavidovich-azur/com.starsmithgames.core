using System.Linq;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace StarSmithGames.Core
{
	public class AssetDatabaseExtensions
    {
#if UNITY_EDITOR
		public static T LoadAsset<T>() where T : ScriptableObject
		{
			return LoadAssets<T>().FirstOrDefault();
		}

		public static T[] LoadAssets<T>(bool orderByName = true) where T : ScriptableObject
		{
			var result = AssetDatabase
				.FindAssets($"t:{typeof(T).Name}")
				.Select((guid) => AssetDatabase.LoadAssetAtPath<T>(AssetDatabase.GUIDToAssetPath(guid)));

			return orderByName ? result.OrderBy((x) => x.name).ToArray() : result.ToArray();
		}
#endif
	}
}