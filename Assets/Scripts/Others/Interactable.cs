using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
	public virtual void EnableInteraction()
	{
		InteractionManager.Instance.closeInteractables.Add(this);
	}

	public virtual void DisableInteraction()
	{
		InteractionManager.Instance.closeInteractables.Remove(this);
	}

	public virtual void Interact()
	{
		DisableInteraction();
	}
}
