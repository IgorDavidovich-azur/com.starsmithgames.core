using Microsoft.Win32;

using StarSmithGames.Core.StorageSystem;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using UnityEditor;

using UnityEngine;

namespace StarSmithGames.Core.Editor.StorageSystem
{
	public class StorageWindow : EditorWindow
	{
		private static StorageWindow window;

		[MenuItem("Tools/Storage Window", priority = 0)]
		public static void ManageData()
		{
			//Window
			window = GetWindow<StorageWindow>(title: "Storage Window", focus: true, utility: true);
			window.maxSize = new Vector2(250, 120);
			window.minSize = new Vector2(250, 120);
			window.ShowUtility();
		}

		private void OnGUI()
		{
			GUILayout.BeginVertical();

			var files = Directory.GetFiles(Application.persistentDataPath).Where((x) => !x.EndsWith(".log") && !x.EndsWith("log.txt")).ToArray();
			var directories = Directory.GetDirectories(Application.persistentDataPath);

			GUI.enabled = files.Length != 0 || directories.Length != 0;

			if (GUILayout.Button("Очистить AppData"))
			{
				foreach (var directory in directories)
				{
					new DirectoryInfo(directory).Delete(true);
				}

				foreach (string filePath in files)
				{
					File.Delete(filePath);
				}

				EditorGUI.FocusTextInControl(null);
			}

			if (GUILayout.Button("Очистить PersistentDataPath"))
			{
				JsonSerializator.ClearPersistentPath();

				EditorGUI.FocusTextInControl(null);
			}

			GUI.enabled = true;

			if (GUILayout.Button("Очистить PlayerPrefs"))
			{
				PlayerPrefs.DeleteAll();
				PlayerPrefs.Save();

				EditorGUI.FocusTextInControl(null);
			}

			GUILayout.EndVertical();

			GUILayout.FlexibleSpace();

			var saveKeys = GetPrefs();//AssetDatabaseExtensions.LoadAsset<StorageManagerInstaller>().playerPrefsSettings.GetKeyList();

			GUI.enabled = saveKeys.Count > 0;

			GUILayout.Label("regedit");

			if (GUILayout.Button("Open PlayerPrefs"))
			{
				var jsonWindow = GetWindow<JsonText>(title: "Json");
				jsonWindow.minSize = new Vector2(400, 700);
				jsonWindow.texts = saveKeys;
			}
		}


		private Dictionary<string, string> GetPrefs()
		{
			Dictionary<string, string> prefs = new();

			try
			{
				//HKEY_CURRENT_USER\SOFTWARE\Unity\UnityEditor\CompanyName\ProductName
				using (RegistryKey registry = Registry.CurrentUser.OpenSubKey($"Software\\Unity\\UnityEditor\\{Application.companyName}\\{Application.productName}"))
				{
					if (registry != null)
					{
						var keys = registry.GetValueNames();
						for (int i = 0; i < keys.Length; i++)
						{
							var key = keys[i].Split("_h")[0];
							if (PlayerPrefs.HasKey(key))
							{
								prefs.Add(key, PlayerPrefs.GetString(key));
							}
						}
					}
				}
			}
			catch (Exception e) { }

			return prefs;
		}
	}

	public class JsonText : EditorWindow
	{
		public Dictionary<string, string> texts;
		public Vector2 scroll = new Vector2(0, 0);

		private void OnGUI()
		{
			scroll = EditorGUILayout.BeginScrollView(scroll, true, true);
			foreach (var item in texts)
			{
				EditorGUILayout.LabelField(item.Key);
				EditorGUILayout.TextArea(item.Value);
			}
			EditorGUILayout.EndScrollView();
		}
	}
}