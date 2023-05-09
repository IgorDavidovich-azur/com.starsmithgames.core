using System;
using System.Collections.Generic;

using UnityEngine;

namespace StarSmithGames.Core.StorageSystem
{
	public class Database : MonoBehaviour
	{
		private Dictionary<string, object> Data = new Dictionary<string, object>();

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

		public T Get<T>(string key, T defaultValue = default)
		{
			if (Data.ContainsKey(key))
			{
				object data = Data[key];

				if (data is T)
				{
					return (T)data;
				}
				try
				{
					return (T)Convert.ChangeType(data, typeof(T));
				}
				catch (InvalidCastException)
				{
					return default(T);
				}
			}
			return defaultValue;
		}

		public void Set<T>(string key, T value)
		{
			Data[key] = value;
		}

		public Dictionary<string, object> GetDictionary() => Data;

		public string GetJson()
		{
			return JsonSerializator.SerializeObjectToJson(Data);
		}
		public void LoadJson(string json)
		{
			Data = JsonSerializator.DeserializeObjectFromJson(json);
		}

		public void Drop()
		{
			Data = new Dictionary<string, object>();
		}
	}
}