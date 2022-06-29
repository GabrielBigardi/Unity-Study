using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionManager : MonoBehaviour
{
	public List<IInteractable> ClosestInteractables = new();

	IEnumerator coroutine;

	private void OnEnable()
	{
		GameEvents.PlayerMoveStartEvent += ResetInteractables;
		GameEvents.PlayerMoveEndEvent += CheckInteractableTiles;
		GameEvents.PlayerCloseToInteractable += AddCloseInteractable;
		GameEvents.PlayerInteractionInput += HandleInteractions;
	}

	private void OnDisable()
	{
		GameEvents.PlayerMoveStartEvent -= ResetInteractables;
		GameEvents.PlayerMoveEndEvent -= CheckInteractableTiles;
		GameEvents.PlayerCloseToInteractable -= AddCloseInteractable;
		GameEvents.PlayerInteractionInput -= HandleInteractions;
	}

	private void CheckInteractableTiles(Vector2 endPosition)
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
			var interactable = Physics2D.OverlapCircle(positionToCheck, 0.1f);
			if (interactable != null && interactable.TryGetComponent(out IInteractable interactableObj))
			{
				interactableObj.EnableInteraction();
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
