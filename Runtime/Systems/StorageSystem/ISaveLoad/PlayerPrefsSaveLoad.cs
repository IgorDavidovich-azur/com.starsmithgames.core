using UnityEngine;

namespace StarSmithGames.Core.StorageSystem
{
	public class PlayerPrefsSaveLoad<S> : ISaveLoad<S> where S : Storage, new()
	{
		private S activeStorage;

		private string dataName;

		public PlayerPrefsSaveLoad(string dataName, bool initLoad = true)
		{
			this.dataName = dataName;

			if (initLoad)
			{
				Load();
			}
		}

		public void Save()
		{
			PlayerPrefs.SetString(dataName, activeStorage.Database.GetSerializedJson());
			PlayerPrefs.Save();

			Debug.Log($"[StorageSystem>PlayerPrefsSaveLoad] Save storage to pref");
		}

		public void Load()
		{
			if (PlayerPrefs.HasKey(dataName))
			{
				string data = PlayerPrefs.GetString(dataName);

				activeStorage = new S().SetData(data) as S;
				activeStorage.IsFirstTime.SetData(false);
			}
			else//first time
			{
				activeStorage = new S();

				Debug.Log($"[StorageSystem>PlayerPrefsSaveLoad] Create new save");
			}

			Debug.Log($"[StorageSystem>PlayerPrefsSaveLoad] Load storage from pref: {dataName}");
		}

		public S GetStorage()
		{
			if (activeStorage == null)
			{
				Load();
			}

			return activeStorage;
		}
	}
}