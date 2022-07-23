using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Fish
{
	public string Name;
	public string Description;
	public int BasePrice;
	public int BaseExp;
	public int TimeRangeMin;
	public int TimeRangeMax;
	public int InchesMinSize;
	public int InchesMaxSize;
	public int Difficulty;
}

public class PoolInteractable : MonoBehaviour, IInteractable
{
	[NonReorderable] public List<Fish> FishesInPool;

	public bool CanBeInteracted = true;

	private void OnEnable()
	{
		GameEvents.PlayerFishingEnded += OnPlayerFishingEnded;
	}

	private void OnDisable()
	{
		GameEvents.PlayerFishingEnded -= OnPlayerFishingEnded;
	}

	public void EnableInteraction()
	{
		if (!CanBeInteracted) return;

		GameEvents.PlayerCloseToInteractable?.Invoke(this);
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

		GameEvents.PlayerFishingStarted?.Invoke(FishesInPool);
		DisableInteraction();
		CanBeInteracted = false;
	}

	void OnPlayerFishingEnded()
	{
		CanBeInteracted = true;
	}
}
