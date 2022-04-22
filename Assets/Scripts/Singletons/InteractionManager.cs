using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionManager : Singleton<InteractionManager>
{
	public List<IInteractable> closeInteractables = new();

	IEnumerator coroutine;

	private void OnEnable()
	{
		GameEvents.PlayerMoveStartEvent += ResetInteractables;
		GameEvents.PlayerMoveEndEvent += CheckInteractableTiles;
	}

	private void OnDisable()
	{
		GameEvents.PlayerMoveStartEvent -= ResetInteractables;
		GameEvents.PlayerMoveEndEvent -= CheckInteractableTiles;
	}

	private void CheckInteractableTiles(Vector2 endPosition)
	{
		if (coroutine != null)
			StopCoroutine(coroutine);

		closeInteractables.Clear();

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

		foreach (var interactable in closeInteractables)
		{
			interactable.DisableInteraction();
		}

		closeInteractables.Clear();
	}

	public void HandleInteractions(InputAction.CallbackContext ctx)
	{
		if (ctx.started)
		{
			for (int i = closeInteractables.Count - 1; i >= 0; i--)
			{
				closeInteractables[i].Interact();
			}
		}
	}
}
