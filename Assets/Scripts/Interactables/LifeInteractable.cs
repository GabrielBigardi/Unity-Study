using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeInteractable : MonoBehaviour, IInteractable
{
	private bool _canBeInteracted = true;
	public bool CanBeInteracted => _canBeInteracted;

	public int lifesToAdd;

	public GameObject interactionSprite;

	public static event Action<LifeInteractable> Interacted;

	public void DisableInteraction()
	{
		interactionSprite.SetActive(false);
	}

	public void EnableInteraction()
	{
		interactionSprite.SetActive(true);
	}

	public void Interact()
	{
		Interacted?.Invoke(this);
	}
}
