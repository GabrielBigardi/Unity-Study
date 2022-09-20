using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public Vector2 MousePositionScreen { get; private set; }
	public Vector2 MousePositionWorld { get; private set; }
    public bool MouseLeftButton { get; private set; }

    public Vector2 RawPlayerInput { get; private set; }
	public Vector2 PlayerInput { get; private set; }

	public static event Action<InputAction.CallbackContext> PlayerInteractionInput;
	public static event Action<InputAction.CallbackContext> PlayerRemoveInventoryInput;

	public void OnMouseMovement(InputAction.CallbackContext ctx)
	{
		MousePositionScreen = ctx.ReadValue<Vector2>();
		MousePositionWorld = Camera.main == null ? Vector3.zero : Camera.main.ScreenToWorldPoint(MousePositionScreen);
	}

    public void OnMouseLeftButton(InputAction.CallbackContext ctx)
	{
        if (ctx.started)
            MouseLeftButton = true;

        if (ctx.canceled)
            MouseLeftButton = false;
	}

	public void OnPlayerInput(InputAction.CallbackContext ctx)
	{
        RawPlayerInput = ctx.ReadValue<Vector2>();

        var oldPlayerInput = PlayerInput;

        PlayerInput = ctx.ReadValue<Vector2>();

        // Disable diagonal movement & prioritize direction of last key pressed
        bool isMovingHorizontal = Mathf.Abs(PlayerInput.x) > 0.5f;
        bool isMovingVertical = Mathf.Abs(PlayerInput.y) > 0.5f;
        bool wasMovingVertical = Mathf.Abs(oldPlayerInput.y) > 0.5f;

        if (isMovingVertical && isMovingHorizontal)
        {
            if (wasMovingVertical)
            {
                PlayerInput = new Vector2(PlayerInput.x, 0f);
            }
            else
            {
                PlayerInput = new Vector2(0f, PlayerInput.y);
            }
        }
        else if (isMovingHorizontal)
        {
            PlayerInput = new Vector2(PlayerInput.x, 0f);
        }
        else if (isMovingVertical)
        {
            PlayerInput = new Vector2(0f, PlayerInput.y);
        }
        else
        {
            PlayerInput = Vector2.zero;
        }
    }

    public void OnPlayerInteraction(InputAction.CallbackContext ctx)
    {
        PlayerInteractionInput?.Invoke(ctx);
    }

	public void OnPlayerRemoveInventory(InputAction.CallbackContext ctx)
	{
		PlayerRemoveInventoryInput?.Invoke(ctx);
	}
}
