using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using GabrielBigardi.SpriteAnimator.Runtime;

public class MovementManager : Singleton<MovementManager>
{
	public LayerMask whatIsNotWalkable;
	public bool isMoving;

	private void Start()
	{

	}

	public void Move(Transform transformToMove, Vector2 whereToMove, float duration)
	{
		isMoving = true;

		GameEvents.PlayerMoveStartEvent?.Invoke(transformToMove.position);

		Sequence s = DOTween.Sequence();
		s.Append(transformToMove.DOMove(whereToMove, duration).SetEase(Ease.Linear));
		s.OnComplete(() => OnMovementComplete(whereToMove));
	}

	private void OnMovementComplete(Vector2 whereToMove)
	{
		isMoving = false;

		GameEvents.PlayerMoveEndEvent?.Invoke(whereToMove);
	}
}
