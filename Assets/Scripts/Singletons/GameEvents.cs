using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameEvents : MonoBehaviour
{
    public static Action<Vector2> PlayerMoveStartEvent;
    public static Action<Vector2> PlayerMoveEndEvent;
    
    public static Action<ICollectable> PlayerCollectedItem;

    public static Action<InputAction.CallbackContext> PlayerInteractionInput;
    public static Action<IInteractable> PlayerInteractionCompleted;
    public static Action<IInteractable> PlayerCloseToInteractable;
}
