using System.IO;
using System.Linq;

using UnityEngine;

namespace StarSmithGames.Core.StorageSystem
{
    public static class DataCleaner
    {
		public static bool IsAppDataClean
		{
			get
			{
				var files = Directory.GetFiles(Application.persistentDataPath).Where((x) => !x.EndsWith(".log") && !x.EndsWith("log.txt")).ToArray();
				var directories = Directory.GetDirectories(Application.persistentDataPath);

				return files.Length == 0 && directories.Length == 0;
			}
		}

		public static void ClearPlayerPrefs()
		{
			PlayerPrefs.DeleteAll();
			PlayerPrefs.Save();
		}

		public static void ClearAppData()
		{
			var files = Directory.GetFiles(Application.persistentDataPath);
			var directories = Directory.GetDirectories(Application.persistentDataPath);

			foreach (var directoryPath in directories)
			{
				Directory.Delete(directoryPath, true);
			}

			foreach (string filePath in files)
			{
				File.Delete(filePath);
			}
		}
	}
}