using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeInteractable : MonoBehaviour, IInteractable
{
	public int lifesToAdd;

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
		LifeManager.Instance.AddLifes(lifesToAdd);
	}
}
