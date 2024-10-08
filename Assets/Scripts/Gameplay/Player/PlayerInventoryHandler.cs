using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GBD.SaveSystem;
using System;
using System.Linq;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

[System.Serializable]
public class PlayerInventory
{
	public List<string> ItemGUIDs = new();
	public List<int> ItemAmount = new();
}

public class PlayerInventoryHandler : MonoBehaviour
{
	public PlayerInventory PlayerInventory;
	public List<InventorySlot> InventorySlots = new List<InventorySlot>();
	[SerializeField] private Image _selectedItemOutline;

	public static event Action<PlayerInventory> PlayerInventoryUpdated;

	private int _selectedItemIndex = 0;

	private void OnEnable()
	{
		SaveSystem.GameDataLoaded += OnGameDataLoaded;
		PlayerFishingHandler.PlayerCatchedFish += AddItem;
		ItemAdderInteractable.Interacted += AddItem;
		PlayerInputHandler.PlayerRemoveInventoryInput += OnPlayerRemoveInventoryInput;
		PlayerInputHandler.PlayerScrollUpInput += OnPlayerScrollUpInput;
		PlayerInputHandler.PlayerScrollDownInput += OnPlayerScrollDownInput;
		PlayerInputHandler.PlayerMouseInput += OnPlayerMouseInput;
	}

	private void OnDisable()
	{
		SaveSystem.GameDataLoaded -= OnGameDataLoaded;
		PlayerFishingHandler.PlayerCatchedFish -= AddItem;
		ItemAdderInteractable.Interacted -= AddItem;
		PlayerInputHandler.PlayerRemoveInventoryInput -= OnPlayerRemoveInventoryInput;
		PlayerInputHandler.PlayerScrollUpInput -= OnPlayerScrollUpInput;
		PlayerInputHandler.PlayerScrollDownInput -= OnPlayerScrollDownInput;
		PlayerInputHandler.PlayerMouseInput -= OnPlayerMouseInput;
	}

	private void Start()
	{
		SelectItemIndex(0);
	}

	private void OnPlayerMouseInput(InputAction.CallbackContext ctx)
	{
		if (ctx.started)
		{
			if (_selectedItemIndex < PlayerInventory.ItemGUIDs.Count && PlayerInventory.ItemGUIDs[_selectedItemIndex] != null)
			{
				var selectedItem = DatabaseManager.Instance.FindItemByGUID(PlayerInventory.ItemGUIDs[_selectedItemIndex]);
				Debug.Log($"Trying to use {selectedItem.Name} at index {_selectedItemIndex}");
				if(selectedItem is IUsable itemUsable)
				{
					var usedSucessfully = itemUsable.Use(GetComponent<PlayerCore>());
					if (usedSucessfully && itemUsable.RemoveOnUse)
					{
						var itemIndex = PlayerInventory.ItemGUIDs.FindIndex(x => x == selectedItem.GUID);
						PlayerInventory.ItemAmount[itemIndex]--;

						if (PlayerInventory.ItemAmount[itemIndex] <= 0)
						{
							PlayerInventory.ItemGUIDs.Remove(selectedItem.GUID);
							PlayerInventory.ItemAmount.RemoveAt(itemIndex);
							RefreshInventory();
						}
						else
						{
							//InventorySlots.Find(x => x.SlotItem == selectedItem).RefreshSlot();
							RefreshInventory();
						}
					}
				}
			}
		}
	}

	private void OnPlayerScrollDownInput(InputAction.CallbackContext ctx)
	{
		if(ctx.started)
			SelectItemIndex(_selectedItemIndex + 1);
	}

	private void OnPlayerScrollUpInput(InputAction.CallbackContext ctx)
	{
		if (ctx.started)
			SelectItemIndex(_selectedItemIndex - 1);
	}

	private void OnPlayerRemoveInventoryInput(InputAction.CallbackContext ctx)
	{
		if (ctx.started)
		{
			ClearInventory();
		}
	}

	public void OnGameDataLoaded(object loadedJson)
	{
		var loadedData = loadedJson as SaveData;
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
				PlayerInventory.ItemAmount.Add(1);
				PlayerInventoryUpdated?.Invoke(PlayerInventory);
				return true;
			}
			else if(slot.SlotItem.Name == addedItem.Name)
			{
				slot.SlotAmount += 1;
				slot.RefreshSlot();
				PlayerInventory.ItemAmount[PlayerInventory.ItemGUIDs.FindIndex(x => x == addedItem.GUID)] += 1;
				//PlayerInventory.ItemGUIDs.Add(addedItem.GUID);
				PlayerInventoryUpdated?.Invoke(PlayerInventory);
				return true;
			}
		}

		return false;
	}

	public void RemoveItem(string itemGUID) => RemoveItem(DatabaseManager.Instance.FindItemByGUID(itemGUID));

	public void RemoveItem(Item removedItem)
	{
		var itemToRemove = PlayerInventory.ItemGUIDs.FirstOrDefault(x => x == removedItem.GUID);
		if (itemToRemove != null)
			PlayerInventory.ItemGUIDs.Remove(itemToRemove);

		Debug.Log($"{removedItem.Name} was removed from the player inventory.");
	}

	private void LoadUIInventorySlots()
	{
		// Populate inventory
		foreach (string itemID in PlayerInventory.ItemGUIDs)
		{
			if (string.IsNullOrEmpty(itemID))
				continue;

			var itemToCheck = DatabaseManager.Instance.FindItemByGUID(itemID);

			foreach (var inventorySlot in InventorySlots)
			{
				if (inventorySlot.SlotItem == null)
				{
					inventorySlot.SlotItem = itemToCheck;
					inventorySlot.SlotAmount = PlayerInventory.ItemAmount[PlayerInventory.ItemGUIDs.FindIndex(x => x == itemID)];
					inventorySlot.RefreshSlot();
					break;
				}else if (inventorySlot.SlotItem.GUID == itemToCheck.GUID)
				{
					inventorySlot.SlotAmount = PlayerInventory.ItemAmount[PlayerInventory.ItemGUIDs.FindIndex(x => x == itemID)];
					inventorySlot.RefreshSlot();
					break;
				}
			}
		}
	}

	private void ClearInventory()
	{
		PlayerInventory.ItemGUIDs.Clear();
		PlayerInventory.ItemAmount.Clear();

		foreach (var inventorySlot in InventorySlots)
		{
			inventorySlot.SlotItem = null;
			inventorySlot.SlotAmount = 0;
			inventorySlot.RefreshSlot();
		}
	}

	public void RefreshInventory()
	{
		foreach (var inventorySlot in InventorySlots)
		{
			inventorySlot.SlotItem = null;
			inventorySlot.SlotAmount = 0;
			inventorySlot.RefreshSlot();
		}

		LoadUIInventorySlots();
	}

	private void SelectItemIndex(int index)
	{
		if (index > InventorySlots.Count - 1)
		{
			index = 0;
		}else if (index < 0)
		{
			index = InventorySlots.Count - 1;
		}

		_selectedItemIndex = index;

		_selectedItemOutline.rectTransform.anchoredPosition = InventorySlots[index].GetComponent<RectTransform>().anchoredPosition;
	}
}
