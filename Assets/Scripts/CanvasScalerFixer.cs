using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.UI;

[ExecuteInEditMode]
public class CanvasScalerFixer : MonoBehaviour
{
	private int lastScreenWidth = 0;
	private int lastScreenHeight = 0;

	[SerializeField] private PixelPerfectCamera _pixelPerfectCamera;
	[SerializeField] private CanvasScaler _canvasScaler;

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
		yield return new WaitForSeconds(0.001f);
	
		float difference = Screen.height / _pixelPerfectCamera.orthographicSize;
		float newScaleFactor = difference / (_pixelPerfectCamera.assetsPPU * 2);
		_canvasScaler.scaleFactor = newScaleFactor;
	}
}
