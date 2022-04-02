using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flip : MonoBehaviour
{
    SpriteRenderer playerSpriteRenderer;
    VerletLauncher playerVerletLauncher;

    // Start is called before the first frame update
    void Awake()
    {
        playerSpriteRenderer = GetComponent<SpriteRenderer>();
        playerVerletLauncher = GetComponent<VerletLauncher>();
    }

    // Update is called once per frame
    void Update()
    {
        playerSpriteRenderer.flipX = playerVerletLauncher.velocityX > 0 ? false : true;
        //if (InputHandler.Instance.PlayerInput.x > 0)
        //    playerSpriteRenderer.flipX = false;
        //
        //if (InputHandler.Instance.PlayerInput.x < 0)
        //    playerSpriteRenderer.flipX = true;
    }
}
