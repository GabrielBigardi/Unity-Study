using GBD.SaveSystem;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public static class GameEvents
{
    // Movement
    public static Action<Vector2> PlayerMoveStartEvent;
    public static Action<Vector2> PlayerMoveEndEvent;
    
    // Player Actions
    public static Action<ICollectable> PlayerCollectedItem;

    // Interaction
    public static Action<InputAction.CallbackContext> PlayerInteractionInput;
    public static Action<IInteractable> PlayerInteractionCompleted;
    public static Action<IInteractable> PlayerCloseToInteractable;

    // Player Health System
    public static Action<int> PlayerLifeChanged;
    public static Action PlayerDeath;

    // Player Fishing System
    public static Action<List<FishItem>> PlayerFishingStarted;
    public static Action PlayerFishingEnded;

    // Player Inventory System
    public static Action<Item> PlayerItemAdded;
    public static Action<Item> PlayerItemRemoved;
    public static Action<PlayerInventory> PlayerInventoryUpdated;
}
