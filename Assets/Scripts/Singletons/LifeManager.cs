using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LifeManager : MonoBehaviour
{
    public int CurrentLifes { get; private set; }

	[SerializeField] private GameObject lifeText;

    private void OnEnable()
    {
		GameEvents.PlayerInteractionCompleted += HandlePlayerInteraction;
		GameEvents.PlayerCollectedItem += HandlePlayerCollect;
    }

    private void OnDisable()
    {
		GameEvents.PlayerInteractionCompleted -= HandlePlayerInteraction;
		GameEvents.PlayerCollectedItem -= HandlePlayerCollect;
	}

    private void Start()
	{
		CurrentLifes = 1;
	}

	public void AddLifes(int amount)
	{
		CurrentLifes += amount;
		RefreshLifeText();
	}

	public void RemoveLifes(int amount)
	{
		CurrentLifes -= amount;
		RefreshLifeText();
	}

	public void RefreshLifeText()
	{
		lifeText.SetTextOutline(CurrentLifes.ToString());
	}

	public void HandlePlayerInteraction(IInteractable interactable)
    {
		if(interactable is LifeInteractable lifeInteractable)
			AddLifes(lifeInteractable.lifesToAdd);
	}

	public void HandlePlayerCollect(ICollectable collectable)
    {
		if (collectable is LifeCollectable lifeCollectable)
			AddLifes(1);
    }
}
