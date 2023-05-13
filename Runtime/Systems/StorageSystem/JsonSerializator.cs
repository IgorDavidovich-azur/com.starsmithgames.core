using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

namespace StarSmithGames.Core.StorageSystem
{
	public class JsonSerializator : MonoBehaviour
	{
		#region Unity Serialization
		public static string SerializeToUnityJson(object data)
		{
			return JsonUtility.ToJson(data, true);
		}

		public static T DeserializeUnityJson<T>(string json)
		{
			return JsonUtility.FromJson<T>(json);
		}
		#endregion

		#region Newtonsoft Serialization
		public static string SerializeObjectToJson(Dictionary<string, object> data)
		{
			return JsonConvert.SerializeObject(data,
			new JsonSerializerSettings()
			{
				Formatting = Formatting.Indented,
				ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
				TypeNameHandling = TypeNameHandling.Auto,
			});
		}

		public static Dictionary<string, object> DeserializeObjectJson(string json)
		{
			return JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
		}
		#endregion
	}
}