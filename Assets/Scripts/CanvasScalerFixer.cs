using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.UI;

public class CanvasScalerFixer : MonoBehaviour
{
	private int lastScreenWidth = 0;
	private int lastScreenHeight = 0;

	void Update()
    {
		if (lastScreenWidth != Screen.width || lastScreenHeight != Screen.height)
		{
			lastScreenWidth = Screen.width;
			lastScreenHeight = Screen.height;
			StartCoroutine(FindCanvasScaleFactor());
		}
	}

	private IEnumerator FindCanvasScaleFactor()
	{
		var pixelPerfectCamera = FindObjectOfType<PixelPerfectCamera>();
		var canvasScaler = FindObjectOfType<CanvasScaler>();
	
		yield return new WaitForSeconds(0.001f);
	
		float difference = Screen.height / pixelPerfectCamera.orthographicSize;
		float newScaleFactor = difference / (pixelPerfectCamera.assetsPPU * 2);
		canvasScaler.scaleFactor = newScaleFactor;
		//Debug.Log(newScaleFactor);
	}
}
