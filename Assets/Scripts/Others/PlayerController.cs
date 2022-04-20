using GabrielBigardi.SpriteAnimator.Runtime;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
	public Vector2 movePos;
	public SpriteAnimator spriteAnimator;
	public float moveDuration;

	private void Update()
	{
		if (InputHandler.Instance.PlayerInput != Vector2.zero && !MovementManager.Instance.isMoving)
		{
			movePos = (Vector2)transform.position + InputHandler.Instance.PlayerInput;
			bool canWalk = !Physics2D.OverlapCircle(movePos, 0.1f, MovementManager.Instance.whatIsNotWalkable);
	
			if (canWalk)
			{
				Vector2 normalizedDistance = (movePos - (Vector2)transform.position).normalized;
	
				if (normalizedDistance == Vector2.up)
					spriteAnimator.PlayIfNotPlaying("Up");
				if (normalizedDistance == Vector2.down)
					spriteAnimator.PlayIfNotPlaying("Down");
				if (normalizedDistance == Vector2.left)
					spriteAnimator.PlayIfNotPlaying("Left");
				if (normalizedDistance == Vector2.right)
					spriteAnimator.PlayIfNotPlaying("Right");
	
				MovementManager.Instance.Move(transform, movePos, moveDuration);
			}
		}
	}

	private void OnDrawGizmos()
	{
		Gizmos.DrawWireSphere(movePos, 0.1f);
	}
}
