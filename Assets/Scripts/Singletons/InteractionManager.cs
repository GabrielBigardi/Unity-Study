using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.InputSystem;
using UnityEngine.Experimental.Rendering.Universal;

public class InteractionManager : Singleton<InteractionManager>
{
	public List<Interactable> interactables;
	public List<Interactable> closeInteractables;
	public PixelPerfectCamera pixelPerfectCamera;

	IEnumerator coroutine;

	private void Start()
	{
		interactables = FindObjectsOfType<Interactable>().ToList();
	}

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
		//Debug.Log($"Player ended at {endPosition}, checking all four directions.");

		var interactableUp = Physics2D.OverlapCircle(endPosition + Vector2.up, 0.1f);
		var interactableDown = Physics2D.OverlapCircle(endPosition + Vector2.down, 0.1f);
		var interactableLeft = Physics2D.OverlapCircle(endPosition + Vector2.left, 0.1f);
		var interactableRight = Physics2D.OverlapCircle(endPosition + Vector2.right, 0.1f);

		if (interactableUp != null && interactableUp.TryGetComponent(out Interactable interactableUpObj))
		{
			interactableUpObj.EnableInteraction();
		}

		if (interactableDown != null && interactableDown.TryGetComponent(out Interactable interactableDownObj))
		{
			interactableDownObj.EnableInteraction();
		}

		if (interactableLeft != null && interactableLeft.TryGetComponent(out Interactable interactableLeftObj))
		{
			interactableLeftObj.EnableInteraction();
		}

		if (interactableRight != null && interactableRight.TryGetComponent(out Interactable interactableRightObj))
		{
			interactableRightObj.EnableInteraction();
		}
	}

	private void ResetInteractables(Vector2 endPosition)
	{
		if(coroutine != null)
			StopCoroutine(coroutine);

		closeInteractables.Clear();

		foreach (var interactable in interactables)
		{
			interactable.DisableInteraction();
		}
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
