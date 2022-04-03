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

		//If the movement is horizontal, make thinner, else make thicker

		Vector3 distance = whereToMove - (Vector2)transformToMove.position;

		Sequence s = DOTween.Sequence();

		if (Mathf.Abs(distance.x) > 0)
		{
			s.Append(transformToMove.DOMove(whereToMove, duration).SetEase(Ease.Linear));
			s.Join(transformToMove.DOScaleX(0.8f, duration / 2));
			s.Join(transformToMove.DOScaleY(1.2f, duration / 2));
			
		}
		else
		{
			s.Append(transformToMove.DOMove(whereToMove, duration).SetEase(Ease.Linear));
			s.Join(transformToMove.DOScaleY(0.8f, duration / 2));
			s.Join(transformToMove.DOScaleX(1.2f, duration / 2));
		}

		s.OnComplete(() => OnMovementComplete(transformToMove, duration / 2));


		//transformToMove.DOMove(whereToMove, duration).SetEase(Ease.Linear).OnComplete(() => OnMovementComplete());
	}

	private void OnMovementComplete(Transform transformToComplete, float duration)
	{
		Sequence s = DOTween.Sequence();
		s.Append(transformToComplete.DOScaleX(1f, duration / 2));
		s.Join(transformToComplete.DOScaleY(1f, duration / 2));

		isMoving = false;
	}
}
