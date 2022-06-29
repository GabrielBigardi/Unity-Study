using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int CurrentLifes { get; private set; }
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

    void Start()
    {
        SetLifes(1);
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
            GameOver();
        }

        GameEvents.PlayerLifeChanged?.Invoke(newLifesAmount);
    }

    public void GameOver()
    {
        Destroy(gameObject);
    }

    public void HandlePlayerInteraction(IInteractable interactable)
    {
        if (interactable is LifeInteractable lifeInteractable)
            AddLifes(lifeInteractable.lifesToAdd);
    }

    public void HandlePlayerCollect(ICollectable collectable)
    {
        if (collectable is LifeCollectable lifeCollectable)
            AddLifes(1);
    }
}
