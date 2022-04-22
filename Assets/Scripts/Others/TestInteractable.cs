using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInteractable : MonoBehaviour, IInteractable
{
	public GameObject interactionSprite;

	public void DisableInteraction()
	{
		//InteractionManager.Instance.closeInteractables.Remove(this);
		interactionSprite.SetActive(false);
	}

	public void EnableInteraction()
	{
		InteractionManager.Instance.closeInteractables.Add(this);
		interactionSprite.SetActive(true);
	}

	public void Interact()
	{
		//DisableInteraction();
	}
}
