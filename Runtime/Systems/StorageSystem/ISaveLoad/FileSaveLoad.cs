using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

using UnityEngine;

namespace StarSmithGames.Core.StorageSystem
{
	/// <summary>
	/// Stub
	/// </summary>
	public class FileSaveLoad<S> : ISaveLoad<S> where S : Storage, new()
	{
		private S activeStorage;

		private string dataName;

		public void Save()
		{
		}

		public void Load()
		{
		}

		public S GetStorage()
		{
			throw new System.NotImplementedException();
		}

		/*
		#region Application.persistentDataPath
		public static void SaveBytesToFile(byte[] bytes, string filepath)
		{
			string dir = Application.persistentDataPath + "/Data/";

			if (!Directory.Exists(dir))
			{
				Directory.CreateDirectory(dir);
			}

			File.WriteAllBytes(dir + filepath, bytes);
		}

		public static byte[] LoadBytesFromFile(string filepath)
		{
			string dir = Application.persistentDataPath + "/Data/";
			return File.ReadAllBytes(dir + filepath);
		}
		#endregion

		private static void SaveDataToJsonUtilityFile<T>(T data, string directory, string fileName)
		{
			string dir = Application.persistentDataPath + directory;

			if (!Directory.Exists(dir))
			{
				Directory.CreateDirectory(dir);
			}

			string jsonData = JsonUtility.ToJson(data, true);
			File.WriteAllText(dir + fileName, jsonData);
		}

		private static T LoadDataFromJsonUtilityFile<T>(string directory, string fileName)
		{
			string fullPath = Application.persistentDataPath + directory + fileName;

			if (File.Exists(fullPath))
			{
				string json = File.ReadAllText(fullPath);
				return JsonUtility.FromJson<T>(json);
			}

			throw new Exception("File doesn't exist");
		}
		private static T LoadDataFromJsonUtilityFile<T>(string fullPath)
		{
			if (File.Exists(fullPath))
			{
				string json = File.ReadAllText(fullPath);
				return JsonUtility.FromJson<T>(json);
			}

			throw new Exception("File doesn't exist");
		}
		*/
	}
}