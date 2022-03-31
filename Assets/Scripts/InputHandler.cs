using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : Singleton<InputHandler>
{
    public Vector2 MousePositionScreen { get; private set; }
	public Vector2 MousePositionWorld { get; private set; }
	public Vector2 PlayerInput { get; private set; }
	public bool LaunchInput { get; private set; }

	public void OnMouseMovement(InputAction.CallbackContext ctx)
	{
		MousePositionScreen = ctx.ReadValue<Vector2>();
		MousePositionWorld = Camera.main.ScreenToWorldPoint(MousePositionScreen);
	}

	public void OnPlayerInput(InputAction.CallbackContext ctx)
	{
		PlayerInput = ctx.ReadValue<Vector2>();
	}
	
	public void OnLaunchInput(InputAction.CallbackContext ctx)
	{
		if (ctx.started)
		{
			LaunchInput = true;
		}else if (ctx.canceled)
		{
			LaunchInput = false;
		}
	}
}
