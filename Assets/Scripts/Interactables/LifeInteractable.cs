using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeInteractable : MonoBehaviour, IInteractable
{
	public int lifesToAdd;

	public GameObject interactionSprite;

	public static event Action<LifeInteractable> Interacted;

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
		Interacted?.Invoke(this);
	}
}
