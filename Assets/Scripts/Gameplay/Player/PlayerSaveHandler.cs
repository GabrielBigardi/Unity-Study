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
		SaveSystem.LoadGame<SaveData>("TestSave", "28472B4B6250655368566D5971337436");
	}

	private void OnApplicationQuit()
	{
		SaveSystem.SaveGame("TestSave", new SaveData(GetComponent<PlayerInventoryHandler>().PlayerInventory), "28472B4B6250655368566D5971337436");
	}
}
