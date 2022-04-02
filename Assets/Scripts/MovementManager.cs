using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using GabrielBigardi.SpriteAnimator.Runtime;

public class MovementManager : Singleton<MovementManager>
{
	public LayerMask whatIsWalkable;
	public bool isMoving;

	public void Move(Transform transformToMove, Vector2 whereToMove, float duration)
	{
		isMoving = true;
		transformToMove.DOMove(whereToMove, duration).SetEase(Ease.Linear).OnComplete(() => OnMovementComplete());
	}

	private void OnMovementComplete()
	{
		isMoving = false;
	}
}
