using Microsoft.Win32;

using StarSmithGames.Core.StorageSystem;

using System;
using System.Collections.Generic;

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

			GUILayout.BeginHorizontal();
			GUI.enabled = !DataCleaner.IsAppDataClean;
			if (GUILayout.Button("Clear AppData"))
			{
				DataCleaner.ClearAppData();

				EditorGUI.FocusTextInControl(null);
			}

			GUI.enabled = true;
			if (GUILayout.Button("Open AppData"))
			{
				var itemPath = Application.persistentDataPath;
				itemPath = itemPath.Replace(@"/", @"\");
				System.Diagnostics.Process.Start("explorer.exe", "/select," + itemPath);
			}
			GUILayout.EndHorizontal();

			GUILayout.BeginHorizontal();
			var saveKeys = GetPrefs();
			GUI.enabled = saveKeys.Count > 0;
			if (GUILayout.Button("Clear PlayerPrefs"))
			{
				DataCleaner.ClearPlayerPrefs();

				EditorGUI.FocusTextInControl(null);
			}

			if (GUILayout.Button("Open PlayerPrefs"))
			{
				var jsonWindow = GetWindow<JsonText>(title: "Json");
				jsonWindow.minSize = new Vector2(400, 700);
				jsonWindow.texts = saveKeys;
			}
			GUILayout.EndHorizontal();

			GUILayout.EndVertical();

			GUILayout.FlexibleSpace();
			GUILayout.Label("regedit");
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