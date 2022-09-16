using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GBD.SaveSystem;
using System;

public class PlayerHealth : MonoBehaviour
{
    [field: SerializeField]
    public int CurrentLifes { get; private set; }

	public static event Action<int> PlayerLifeChanged;
	public static event Action PlayerDeath;

	private void OnEnable()
    {
        LifeInteractable.Interacted += HandlePlayerInteraction;
        LifeCollectable.Collected += HandlePlayerCollect;
    }

    private void OnDisable()
    {
		LifeInteractable.Interacted -= HandlePlayerInteraction;
		LifeCollectable.Collected -= HandlePlayerCollect;
    }

    public void AddLifes(int amount)
    {
        SetLifes(CurrentLifes + amount);
    }

    public void RemoveLifes(int amount)
    {
        SetLifes(CurrentLifes - amount);
    }

    public void SetLifes(int newLifesAmount)
    {
        CurrentLifes = newLifesAmount;
        
        if(newLifesAmount < 0)
        {
            newLifesAmount = 0;
            Death();
        }

        PlayerLifeChanged?.Invoke(newLifesAmount);
    }

    public void Death()
    {
        PlayerDeath?.Invoke();
        Destroy(gameObject);
    }

    public void HandlePlayerInteraction(LifeInteractable lifeInteractable)
    {
        AddLifes(lifeInteractable.lifesToAdd);
    }

    public void HandlePlayerCollect()
    {
        AddLifes(1);
    }
}
