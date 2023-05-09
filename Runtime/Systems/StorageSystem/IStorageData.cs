using System;

namespace StarSmithGames.Core.StorageSystem
{
	public interface IStorageData<T>
	{
		event Action onChanged;

		T GetData();
		void SetData(T data);
	}

	public class StorageData<T> : IStorageData<T>
	{
		public event Action onChanged;

		private Database database;
		private string key;
		private T defaultValue;

		public StorageData(Database database, string key, T defaultValue = default)
		{
			this.database = database;
			this.key = key;
			this.defaultValue = defaultValue;

			SetData(database.Get(GetDataKey(), defaultValue));
		}

		public string GetDataKey()
		{
			return key;
		}

		public T GetData()
		{
			return database.Get<T>(GetDataKey(), defaultValue);
		}

		public void SetData(T data)
		{
			database.Set(GetDataKey(), data);

			onChanged?.Invoke();
		}
	}

	public abstract class Storage
	{
		public Database Database { get; private set; }

		public IStorageData<bool> IsFirstTime { get; private set; }

		/// <summary>
		/// Default Data
		/// </summary>
		public Storage()
		{
			Database = new Database();

			Initialization();
		}

		/// <summary>
		/// Json Data
		/// </summary>
		public Storage(string data)
		{
			Database = new Database();
			Database.LoadJson(data);

			Initialization();
		}

		public Storage SetData(string data)
		{
			Database.LoadJson(data);

			Initialization();

			return this;
		}

		public void Clear()
		{
			Database.Drop();
			Initialization();
		}

		protected virtual void Initialization()
		{
			Purge();

			IsFirstTime = new StorageData<bool>(Database, "is_first_time", true);
		}

		/// <summary>
		/// Clear old keys
		/// Database.Remove("language_index");//v1.0.3
		/// </summary>
		protected abstract void Purge();
	}
}