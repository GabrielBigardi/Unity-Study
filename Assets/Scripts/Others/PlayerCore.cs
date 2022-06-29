using GabrielBigardi.SpriteAnimator.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCore : MonoBehaviour
{
    [field: SerializeField] public PlayerCollisionSenses PlayerCollisionSenses{ get; private set; }
    [field: SerializeField] public PlayerMovement PlayerMovement { get; private set; }
    [field: SerializeField] public PlayerInputHandler PlayerInputHandler { get; private set; }
    [field: SerializeField] public SpriteAnimator SpriteAnimator { get; private set; }
    [field: SerializeField] public PlayerHealth PlayerHealth { get; private set; }
}
