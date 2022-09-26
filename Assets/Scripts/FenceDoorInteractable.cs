using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FenceDoorInteractable : MonoBehaviour, IInteractable
{
	private bool _isOpen = false;

	private bool _canBeInteracted = true;
	public bool CanBeInteracted => _canBeInteracted;

	[SerializeField] private Sprite _closedSprite;
	[SerializeField] private Sprite _openedSprite;
	[SerializeField] private BoxCollider2D _boxCollider2D;
	//public static event Action<LifeInteractable> Interacted;

	public void EnableInteraction()
	{
		if (!CanBeInteracted) return;

		GetComponent<SpriteRenderer>().material.SetFloat("_OutlineThickness", 1f);
	}

	public void DisableInteraction()
	{
		GetComponent<SpriteRenderer>().material.SetFloat("_OutlineThickness", 0f);
	}

	public void Interact()
	{
		_isOpen = !_isOpen;

		if (_isOpen)
		{
			GetComponent<SpriteRenderer>().sprite = _openedSprite;
			_boxCollider2D.enabled = false;
		}
		else
		{
			GetComponent<SpriteRenderer>().sprite = _closedSprite;
			_boxCollider2D.enabled = true;
		}
		//Interacted?.Invoke(this);
	}
}
