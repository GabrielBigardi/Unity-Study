using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static Action<Vector2> PlayerMoveStartEvent;
    public static Action<Vector2> PlayerMoveEndEvent;
    public static Action PlayerCollectedItem;
}
