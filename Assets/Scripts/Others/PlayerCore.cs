using GabrielBigardi.SpriteAnimator.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCore : MonoBehaviour
{
    [field: SerializeField] public PlayerController PlayerController { get; private set; }
    [field: SerializeField] public PlayerMovement PlayerMovement { get; private set; }
    [field: SerializeField] public InputHandler PlayerInput { get; private set; }
    [field: SerializeField] public SpriteAnimator SpriteAnimator { get; private set; }
}
