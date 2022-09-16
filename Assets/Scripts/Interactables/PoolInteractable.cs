using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolInteractable : MonoBehaviour, IInteractable
{
	private bool _canBeInteracted = true;
	public bool CanBeInteracted => _canBeInteracted;

	[NonReorderable] public List<FishItem> FishesInPool;

	public static event Action<List<FishItem>> PlayerFishingStarted;

	private void OnEnable()
	{
		PlayerFishingHandler.PlayerFishingEnded += OnPlayerFishingEnded;
	}

	private void OnDisable()
	{
		PlayerFishingHandler.PlayerFishingEnded -= OnPlayerFishingEnded;
	}

	public void EnableInteraction()
	{
		if (!CanBeInteracted) return;

		GetComponent<SpriteRenderer>().material.SetFloat("_OutlineThickness", 1f);
	}

	public void DisableInteraction()
	{
		//GameEvents.PlayerFishingEnded?.Invoke();
		GetComponent<SpriteRenderer>().material.SetFloat("_OutlineThickness", 0f);
	}

	public void Interact()
	{
		if (!CanBeInteracted) return;

		PlayerFishingStarted?.Invoke(FishesInPool);
		DisableInteraction();
		_canBeInteracted = false;
	}

	void OnPlayerFishingEnded()
	{
		_canBeInteracted = true;
	}
}
