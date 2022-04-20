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

	private void FixedUpdate()
	{
		//Camera.main.transform.Translate(Vector2.right * 0.2f * Time.deltaTime);
	}

	//private IEnumerator FindCanvasScaleFactor()
	//{
	//	var pixelPerfectCamera = FindObjectOfType<PixelPerfectCamera>();
	//	var canvasScaler = FindObjectOfType<CanvasScaler>();
	//	var canvas = FindObjectOfType<Canvas>();
	//	var targetPixelSize = 1f / pixelPerfectCamera.assetsPPU;
	//
	//	yield return new WaitForSeconds(0.001f);
	//
	//	float difference = Screen.height / pixelPerfectCamera.orthographicSize;
	//
	//	// find which number that dividing the difference gives 32
	//	for (int i = 1; i < 20; i++)
	//	{
	//		if (difference / i == (pixelPerfectCamera.assetsPPU * 2))
	//		{
	//			Debug.Log($"{difference} -> Scale Factor {i}");
	//			canvasScaler.scaleFactor = i;
	//			break;
	//		}
	//	}
	//}

	private IEnumerator FindCanvasScaleFactor()
	{
		var pixelPerfectCamera = FindObjectOfType<PixelPerfectCamera>();
		var canvasScaler = FindObjectOfType<CanvasScaler>();
	
		yield return new WaitForSeconds(0.001f);
	
		float difference = Screen.height / pixelPerfectCamera.orthographicSize;
		float newScaleFactor = difference / (pixelPerfectCamera.assetsPPU * 2);
		canvasScaler.scaleFactor = newScaleFactor;
		Debug.Log(newScaleFactor);
	}
}
