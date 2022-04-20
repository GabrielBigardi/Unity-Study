using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum InputAxis
{
	X,
	Y
}

public class InputHandler : Singleton<InputHandler>
{
    public Vector2 MousePositionScreen { get; private set; }
	public Vector2 MousePositionWorld { get; private set; }

    public Vector2 RawPlayerInput { get; private set; }
	public Vector2 PlayerInput { get; private set; }

	public void OnMouseMovement(InputAction.CallbackContext ctx)
	{
		MousePositionScreen = ctx.ReadValue<Vector2>();
		MousePositionWorld = Camera.main == null ? Vector3.zero : Camera.main.ScreenToWorldPoint(MousePositionScreen);
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
}
