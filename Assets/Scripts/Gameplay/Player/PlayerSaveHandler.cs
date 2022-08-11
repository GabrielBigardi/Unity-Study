using GBD.SaveSystem;
using UnityEngine;

[System.Serializable]
public class SaveData
{
	public PlayerInventory PlayerInventory;

	public SaveData(PlayerInventory playerInventory)
	{
		this.PlayerInventory = playerInventory;
	}
}

public class PlayerSaveHandler : MonoBehaviour
{
	private void Start()
	{
		SaveSystem.LoadGame<SaveData>("TestSave");
	}

	private void OnApplicationQuit()
	{
		SaveSystem.SaveGame("TestSave", new SaveData(GetComponent<PlayerInventoryHandler>().PlayerInventory));
	}

	//private void OnLoadGame(string loadedJson)
	//{
	//    var loadedData = JsonUtility.FromJson<SaveData>(loadedJson);
	//}
}
