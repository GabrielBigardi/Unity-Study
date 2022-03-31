using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flip : MonoBehaviour
{
    SpriteRenderer playerSpriteRenderer;

    // Start is called before the first frame update
    void Awake()
    {
        playerSpriteRenderer = GameObject.Find("Fire Bomb").GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (InputHandler.Instance.PlayerInput.x > 0)
            playerSpriteRenderer.flipX = false;

        if (InputHandler.Instance.PlayerInput.x < 0)
            playerSpriteRenderer.flipX = true;
    }
}
