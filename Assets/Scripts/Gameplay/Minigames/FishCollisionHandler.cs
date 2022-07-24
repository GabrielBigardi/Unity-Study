using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishCollisionHandler : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("MinigameFish"))
		{
			GetComponentInParent<PlayerFishingHandler>().IsBarTouchingFish = true;
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.CompareTag("MinigameFish"))
		{
			GetComponentInParent<PlayerFishingHandler>().IsBarTouchingFish = false;
		}
	}
}
