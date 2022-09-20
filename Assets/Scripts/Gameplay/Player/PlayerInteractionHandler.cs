using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractionHandler : MonoBehaviour
{
	public LayerMask NotIgnoredLayers;
	public List<IInteractable> ClosestInteractables = new();

	IEnumerator coroutine;

	public static event Action<IInteractable> PlayerCloseToInteractable;

	private void OnEnable()
	{
		PlayerMovement.PlayerMoveStartEvent += ResetInteractables;
		PlayerMovement.PlayerMoveEndEvent += CheckInteractableTiles;
		PlayerInputHandler.PlayerInteractionInput += HandleInteractions;
		PlayerHealth.PlayerDeath += OnPlayerDeath;
		PlayerFishingHandler.PlayerFishingEnded += OnPlayerFishingEnded;
	}

	private void OnDisable()
	{
		PlayerMovement.PlayerMoveStartEvent -= ResetInteractables;
		PlayerMovement.PlayerMoveEndEvent -= CheckInteractableTiles;
		PlayerInputHandler.PlayerInteractionInput -= HandleInteractions;
		PlayerHealth.PlayerDeath -= OnPlayerDeath;
		PlayerFishingHandler.PlayerFishingEnded -= OnPlayerFishingEnded;
	}

	private void OnPlayerDeath()
	{
		ClosestInteractables.ForEach((IInteractable interactable) => interactable.DisableInteraction());
	}

	private void OnPlayerFishingEnded()
	{
		CheckInteractableTiles(transform.position);
	}

	public void CheckInteractableTiles(Vector2 endPosition)
	{
		if (coroutine != null)
			StopCoroutine(coroutine);

		ClosestInteractables.Clear();

		coroutine = CheckInteractableTiles_CR(endPosition);
		StartCoroutine(coroutine);
	}

	IEnumerator CheckInteractableTiles_CR(Vector2 endPosition)
	{
		yield return new WaitForSeconds(0.1f);

		List<Vector2> positionsToCheck = new List<Vector2> { endPosition + Vector2.up, endPosition + Vector2.down, endPosition + Vector2.left, endPosition + Vector2.right };

		foreach (var positionToCheck in positionsToCheck)
		{
			var interactable = Physics2D.OverlapCircle(positionToCheck, 0.1f, NotIgnoredLayers);
			if (interactable != null && interactable.TryGetComponent(out IInteractable interactableObj))
			{
				interactableObj.EnableInteraction();
				AddCloseInteractable(interactableObj);
				PlayerCloseToInteractable?.Invoke(interactableObj);
			}
		}
	}

	private void ResetInteractables(Vector2 endPosition)
	{
		if(coroutine != null)
			StopCoroutine(coroutine);

		foreach (var interactable in ClosestInteractables)
		{
			interactable.DisableInteraction();
		}

		ClosestInteractables.Clear();
	}

	public void HandleInteractions(InputAction.CallbackContext ctx)
	{
		if (ctx.started)
		{
			for (int i = ClosestInteractables.Count - 1; i >= 0; i--)
			{
				ClosestInteractables[i].Interact();
			}
		}
	}

	public void AddCloseInteractable(IInteractable interactable)
    {
		ClosestInteractables.Add(interactable);
	}
}
