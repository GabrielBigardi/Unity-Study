using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GBD.SaveSystem;

[System.Serializable]
public class PlayerInventory
{
	public List<string> ItemGUIDs = new(); 
}

public class PlayerInventoryHandler : MonoBehaviour
{
	public PlayerInventory PlayerInventory;
	public List<InventorySlot> InventorySlots = new List<InventorySlot>();

	private void OnEnable()
	{
		SaveSystem.LoadedGameData += OnLoadedGameData;
		GameEvents.PlayerItemAdded += OnPlayerItemAdded;
		GameEvents.PlayerItemRemoved += OnPlayerItemRemoved;
	}

	private void OnDisable()
	{
		SaveSystem.LoadedGameData -= OnLoadedGameData;
		GameEvents.PlayerItemAdded -= OnPlayerItemAdded;
		GameEvents.PlayerItemRemoved -= OnPlayerItemRemoved;
	}

	public void OnLoadedGameData(string loadedJson)
	{
		var loadedData = JsonUtility.FromJson<SaveData>(loadedJson);
		PlayerInventory = loadedData.PlayerInventory;
		LoadUIInventorySlots();
	}

	public void OnPlayerItemAdded(Item addedItem)
	{
		bool foundSlot = false;
		foreach (var slot in InventorySlots)
		{
			if(slot.SlotItem == null)
			{
				slot.SlotItem = addedItem;
				slot.SlotAmount += 1;
				slot.RefreshSlot();
				foundSlot = true;
				PlayerInventory.ItemGUIDs.Add(addedItem.GUID);
				GameEvents.PlayerInventoryUpdated?.Invoke(PlayerInventory);
				break;
			}
			else if(slot.SlotItem.Name == addedItem.Name)
			{
				slot.SlotAmount += 1;
				slot.RefreshSlot();
				foundSlot = true;
				PlayerInventory.ItemGUIDs.Add(addedItem.GUID);
				GameEvents.PlayerInventoryUpdated?.Invoke(PlayerInventory);
				break;
			}
		}

		Debug.Log(foundSlot ? $"{addedItem.Name} was added to the inventory." : $"{addedItem.Name} couldn't be added, no more slots");
	}

	public void OnPlayerItemRemoved(Item removedItem)
	{
		Debug.Log($"{removedItem.Name} was removed from the player inventory.");
	}

	private void LoadUIInventorySlots()
	{
		// Foreach item in inventory
		for (int i = 0; i < PlayerInventory.ItemGUIDs.Count; i++)
		{
			// If GUID is empty continue to next iteration
			if (string.IsNullOrEmpty(PlayerInventory.ItemGUIDs[i]))
				continue;

			// Find item by GUID in database
			var itemToCheck = DatabaseManager.Instance.FindItemByGUID(PlayerInventory.ItemGUIDs[i]);

			// Loop through each inventory slot
			foreach (var inventorySlot in InventorySlots)
			{
				// If item in current slot is null, add this one
				if (inventorySlot.SlotItem == null)
				{
					inventorySlot.SlotItem = itemToCheck;
					inventorySlot.SlotAmount++;
					inventorySlot.RefreshSlot();
					break;
				}else if (inventorySlot.SlotItem.GUID == itemToCheck.GUID)
				{
					inventorySlot.SlotAmount++;
					inventorySlot.RefreshSlot();
					break;
				}
			}
		}
		//// Foreach slot
		//for (int i = 0; i < InventorySlots.Count; i++)
		//{
		//	// If Player has GUID X and GUID is not empty
		//	if (PlayerInventory.ItemGUIDs.Count > i && !string.IsNullOrEmpty(PlayerInventory.ItemGUIDs[i]))
		//	{
		//		Debug.Log(PlayerInventory.ItemGUIDs[i]);
		//		var itemToCheck = DatabaseManager.Instance.FindItemByGUID(PlayerInventory.ItemGUIDs[i]);
		//		if (InventorySlots[i].SlotItem != null && InventorySlots[i].SlotItem.Name == itemToCheck.Name)
		//		{
		//			InventorySlots[i].SlotAmount++;
		//			InventorySlots[i].RefreshSlot();
		//			
		//		}
		//		else if (InventorySlots[i].SlotItem == null)
		//		{
		//			InventorySlots[i].SlotItem = itemToCheck;
		//			InventorySlots[i].RefreshSlot();
		//		}
		//	}
		//}
	}
}
