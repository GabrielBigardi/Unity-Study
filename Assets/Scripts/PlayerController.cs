using GabrielBigardi.SpriteAnimator.Runtime;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public Vector2 movePos;
	public SpriteAnimator spriteAnimator;

	private void Update()
    {
		if (InputHandler.Instance.PlayerInput != Vector2.zero && Mathf.Abs(InputHandler.Instance.PlayerInput.x + InputHandler.Instance.PlayerInput.y) == 1f && !MovementManager.Instance.isMoving)
		{
			movePos = (Vector2)transform.position + InputHandler.Instance.PlayerInput * 2f;
			bool canWalk = Physics2D.OverlapCircle(movePos, 0.5f, MovementManager.Instance.whatIsWalkable);

			if (canWalk)
			{
				Vector2 normalizedDistance = (movePos - (Vector2)transform.position).normalized;

				if (normalizedDistance == Vector2.up)
					spriteAnimator.Play("PlayerUp");
				if (normalizedDistance == Vector2.down)
					spriteAnimator.Play("PlayerDown");
				if (normalizedDistance == Vector2.left)
					spriteAnimator.Play("PlayerLeft");
				if (normalizedDistance == Vector2.right)
					spriteAnimator.Play("PlayerRight");

				MovementManager.Instance.Move(transform, movePos, 0.3f);
			}
		}
	}

	private void OnDrawGizmos()
	{
		Gizmos.DrawWireSphere(movePos, 0.5f);
	}
}
