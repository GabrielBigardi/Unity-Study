using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseInteractable : MonoBehaviour, IInteractable
{
	private bool _canBeInteracted = true;
	public bool CanBeInteracted => _canBeInteracted;

	public GameObject interactionSprite;

	public static event Action Interacted;

	public void DisableInteraction()
	{
		interactionSprite.SetActive(false);
	}

	public void EnableInteraction()
	{
		if (!CanBeInteracted) return;

		interactionSprite.SetActive(true);
	}

	public void Interact()
	{
		if (!CanBeInteracted) return;

		DisableInteraction();
		Interacted?.Invoke();
		_canBeInteracted = false;
	}
}
