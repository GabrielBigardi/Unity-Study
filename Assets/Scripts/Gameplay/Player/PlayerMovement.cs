using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	private PlayerCore _playerCore;

	public LayerMask WhatIsNotWalkable;
	public bool IsMoving;
	public Vector2 MovePos;
	public float MoveDuration;
	bool CanMove = true;

	public bool CanWalkTo(Vector2 point) => !Physics2D.OverlapCircle(MovePos, 0.1f, WhatIsNotWalkable);

	private void OnEnable()
	{
		GameEvents.PlayerFishingStarted += OnPlayerFishingStarted;
		GameEvents.PlayerFishingEnded += OnPlayerFishingEnded;
	}

	private void OnDisable()
	{
		GameEvents.PlayerFishingStarted -= OnPlayerFishingStarted;
		GameEvents.PlayerFishingEnded -= OnPlayerFishingEnded;
	}

	private void Start()
	{
		_playerCore = GetComponent<PlayerCore>();
	}

	private void Update()
	{
		if (_playerCore.PlayerInputHandler.PlayerInput != Vector2.zero && !IsMoving && CanMove)
		{
			MovePos = (Vector2)transform.position + _playerCore.PlayerInputHandler.PlayerInput;

			if (CanWalkTo(MovePos))
			{
				Vector2 normalizedDistance = (MovePos - (Vector2)transform.position).normalized;

				if (normalizedDistance == Vector2.up)
					_playerCore.SpriteAnimator.PlayIfNotPlaying("Up");
				if (normalizedDistance == Vector2.down)
					_playerCore.SpriteAnimator.PlayIfNotPlaying("Down");
				if (normalizedDistance == Vector2.left)
					_playerCore.SpriteAnimator.PlayIfNotPlaying("Left");
				if (normalizedDistance == Vector2.right)
					_playerCore.SpriteAnimator.PlayIfNotPlaying("Right");

				Move(transform, MovePos, MoveDuration);
			}
		}
	}

	public void Move(Transform transformToMove, Vector2 whereToMove, float duration)
	{
		IsMoving = true;

		GameEvents.PlayerMoveStartEvent?.Invoke(transformToMove.position);

		Sequence s = DOTween.Sequence();
		s.Append(transformToMove.DOMove(whereToMove, duration).SetEase(Ease.Linear));
		s.OnComplete(() => OnMovementComplete(whereToMove));
	}

	private void OnMovementComplete(Vector2 whereToMove)
	{
		IsMoving = false;

		GameEvents.PlayerMoveEndEvent?.Invoke(whereToMove);
	}

	private void OnPlayerFishingStarted(List<Fish> possibleFishes)
	{
		CanMove = false;
	}

	private void OnPlayerFishingEnded()
	{
		CanMove = true;
	}

	private void OnDrawGizmos()
	{
		Gizmos.DrawWireSphere(MovePos, 0.1f);
	}
}
