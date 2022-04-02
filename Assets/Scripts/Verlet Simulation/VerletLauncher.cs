using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerletLauncher : MonoBehaviour
{
    public bool isAttached = true;
    public Vector3 previousPosition;
    public float velocityX = 0f;
    public float velocityY = 0f;

	private void Update()
	{
		if (InputHandler.Instance.LaunchInput && isAttached)
		{
            isAttached = false;
            GameObject.Find("Fire Bomb").GetComponent<Rigidbody2D>().gravityScale = 1f;
            GameObject.Find("Fire Bomb").GetComponent<Rigidbody2D>().velocity = new Vector2(velocityX, velocityY) * 2f;
		}
	}

	void FixedUpdate()
    {
        velocityX = ((transform.position.x - previousPosition.x)) / Time.deltaTime;
        velocityY = ((transform.position.y - previousPosition.y)) / Time.deltaTime;
        previousPosition = transform.position;
    }
}
