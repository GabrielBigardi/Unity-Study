using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishCollisionHandler : MonoBehaviour
{
	public static event Action<bool> FishingBarCollisionWithFish;

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("MinigameFish"))
		{
			FishingBarCollisionWithFish?.Invoke(true);
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.CompareTag("MinigameFish"))
		{
			FishingBarCollisionWithFish?.Invoke(false);
		}
	}
}
