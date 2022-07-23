using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MovingFish : MonoBehaviour
{
	private void OnEnable()
	{
		StartCoroutine("FishMovementCR");
	}

	private void OnDisable()
	{
		StopCoroutine("FishMovementCR");
	}

	IEnumerator FishMovementCR()
    {
		while (true)
		{
			if (gameObject.activeSelf)
			{
				transform.DOLocalMoveY(Random.Range(-1.5625f, 1.5625f), 1f);
				yield return new WaitForSeconds(0.7f);
			}
			else
			{
				yield break;
			}
		}
    }
}
