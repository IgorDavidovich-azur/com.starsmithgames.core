using Newtonsoft.Json.Linq;

using System;
using System.Collections.Generic;

using UnityEngine;

namespace StarSmithGames.Core.StorageSystem
{
	public class Database : MonoBehaviour
	{
		public Dictionary<string, object> Data { get; private set; } = new();

		public T Get<T>(string key, T defaultValue = default)
		{
			if (Data.ContainsKey(key))
			{
				object data = Data[key];

				//fix Error "Object must implement IConvertible"
				if (data is JObject jdata) // >:c
				{
					data = jdata.ToObject<T>();
					Data[key] = data;
				}

				try
				{
					return (T)data;
				}
				catch (InvalidCastException e)
				{
					Debug.LogWarning($"[StorageSystem>Database] {key} default:{defaultValue} {e.Message}");

					try
					{
						return (T)Convert.ChangeType(data, typeof(T));
					}
					catch (Exception e2)
					{
						Debug.LogError($"[StorageSystem>Database] {key} default:{defaultValue} {e2.Message}");

						return defaultValue;
					}
				}
			}

			return defaultValue;
		}

		public void Set<T>(string key, T value)
		{
			Data[key] = value;
		}

		public bool IsHas(string key)
		{
			return Data.ContainsKey(key);
		}

		public void Remove(string key)
		{
			if (IsHas(key))
			{
				Data.Remove(key);
			}
		}

		public string GetSerializedJson()
		{
			return JsonSerializator.SerializeObjectToJson(Data);
		}

		public void DeserializeJson(string json)
		{
			Data = JsonSerializator.DeserializeObjectJson(json);
		}

		public void Drop()
		{
			Data = new Dictionary<string, object>();
		}
	}
}