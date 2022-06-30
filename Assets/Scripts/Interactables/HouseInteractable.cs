using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseInteractable : MonoBehaviour, IInteractable
{
	public GameObject interactionSprite;

	public void DisableInteraction()
	{
		interactionSprite.SetActive(false);
	}

	public void EnableInteraction()
	{
		GameEvents.PlayerCloseToInteractable?.Invoke(this);
		interactionSprite.SetActive(true);
	}

	public void Interact()
	{
		GameEvents.PlayerInteractionCompleted?.Invoke(this);
	}
}
