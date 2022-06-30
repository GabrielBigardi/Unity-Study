using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseInteractable : MonoBehaviour, IInteractable
{
	public bool CanBeInteracted = true;
	public GameObject interactionSprite;

	public void DisableInteraction()
	{
		interactionSprite.SetActive(false);
	}

	public void EnableInteraction()
	{
		if (!CanBeInteracted) return;

		GameEvents.PlayerCloseToInteractable?.Invoke(this);
		interactionSprite.SetActive(true);
	}

	public void Interact()
	{
		if (!CanBeInteracted) return;

		DisableInteraction();
		GameEvents.PlayerInteractionCompleted?.Invoke(this);
		CanBeInteracted = false;
	}
}
