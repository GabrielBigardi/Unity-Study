using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryHandler : MonoBehaviour
{
	public List<InventorySlot> InventorySlots = new List<InventorySlot>();

	private void OnEnable()
	{
		GameEvents.PlayerItemAdded += OnPlayerItemAdded;
		GameEvents.PlayerItemRemoved += OnPlayerItemRemoved;
	}

	private void OnDisable()
	{
		GameEvents.PlayerItemAdded -= OnPlayerItemAdded;
		GameEvents.PlayerItemRemoved -= OnPlayerItemRemoved;
	}

	public void OnPlayerItemAdded(IItem addedItem)
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
				break;
			}
			else if(slot.SlotItem.Name == addedItem.Name)
			{
				slot.SlotAmount += 1;
				slot.RefreshSlot();
				foundSlot = true;
				break;
			}
		}

		Debug.Log(foundSlot ? $"{addedItem.Name} was added to the inventory." : $"{addedItem.Name} couldn't be added, no more slots");
	}

	public void OnPlayerItemRemoved(IItem removedItem)
	{
		Debug.Log($"{removedItem.Name} was removed from the player inventory.");
	}
}
