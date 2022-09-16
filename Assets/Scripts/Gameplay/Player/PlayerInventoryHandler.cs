using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GBD.SaveSystem;
using System;
using System.Linq;

[System.Serializable]
public class PlayerInventory
{
	public List<string> ItemGUIDs = new(); 
}

public class PlayerInventoryHandler : MonoBehaviour
{
	public PlayerInventory PlayerInventory;
	public List<InventorySlot> InventorySlots = new List<InventorySlot>();

	public static event Action<PlayerInventory> PlayerInventoryUpdated;

	private void OnEnable()
	{
		SaveSystem.GameDataLoaded += OnGameDataLoaded;
		PlayerFishingHandler.PlayerCatchedFish += AddItem;
	}

	private void OnDisable()
	{
		SaveSystem.GameDataLoaded -= OnGameDataLoaded;
		PlayerFishingHandler.PlayerCatchedFish -= AddItem;
	}

	public void OnGameDataLoaded(object loadedJson)
	{
		var loadedData = loadedJson as SaveData;
		foreach (var test in loadedData.PlayerInventory.ItemGUIDs)
		{
			Debug.Log(test);
		}
		PlayerInventory = loadedData.PlayerInventory;
		LoadUIInventorySlots();
	}

	public void AddItem(Item itemToAdd) => TryAddItem(itemToAdd);

	public bool TryAddItem(Item addedItem)
	{
		foreach (var slot in InventorySlots)
		{
			if(slot.SlotItem == null)
			{
				slot.SlotItem = addedItem;
				slot.SlotAmount += 1;
				slot.RefreshSlot();
				PlayerInventory.ItemGUIDs.Add(addedItem.GUID);
				PlayerInventoryUpdated?.Invoke(PlayerInventory);
				return true;
			}
			else if(slot.SlotItem.Name == addedItem.Name)
			{
				slot.SlotAmount += 1;
				slot.RefreshSlot();
				PlayerInventory.ItemGUIDs.Add(addedItem.GUID);
				PlayerInventoryUpdated?.Invoke(PlayerInventory);
				return true;
			}
		}

		return false;
	}

	public void RemoveItem(Item removedItem)
	{
		var itemToRemove = PlayerInventory.ItemGUIDs.FirstOrDefault(x => x == removedItem.GUID);
		if (itemToRemove != null)
			PlayerInventory.ItemGUIDs.Remove(itemToRemove);

		Debug.Log($"{removedItem.Name} was removed from the player inventory.");
	}

	private void LoadUIInventorySlots()
	{
		for (int i = 0; i < PlayerInventory.ItemGUIDs.Count; i++)
		{
			if (string.IsNullOrEmpty(PlayerInventory.ItemGUIDs[i]))
				continue;

			var itemToCheck = DatabaseManager.Instance.FindItemByGUID(PlayerInventory.ItemGUIDs[i]);

			foreach (var inventorySlot in InventorySlots)
			{
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
	}
}
