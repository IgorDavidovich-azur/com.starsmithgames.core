namespace StarSmithGames.Core.StorageSystem
{
	public interface ISaveLoad<S> where S : Storage
    {
		void Save();

		void Load();

		/// <summary>
		/// Get currently selected storage 
		/// </summary>
		/// <returns>Current active storage.</returns>
		S GetStorage();
	}
}