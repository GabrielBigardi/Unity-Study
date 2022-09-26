using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAdderInteractable : MonoBehaviour, IInteractable
{
	[SerializeField] private Item _itemToDrop;

	private bool _canBeInteracted = true;
	public bool CanBeInteracted => _canBeInteracted;

	public static event Action<Item> Interacted;

	public void DisableInteraction()
	{
		//interactionSprite.SetActive(false);
	}

	public void EnableInteraction()
	{
		//interactionSprite.SetActive(true);
	}

	public void Interact()
	{
		Interacted?.Invoke(_itemToDrop);
	}
}
