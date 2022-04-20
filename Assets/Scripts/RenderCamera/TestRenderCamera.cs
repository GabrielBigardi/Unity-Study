using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRenderCamera : MonoBehaviour
{
	public int zoomScale;
	public int PPU;
	public Vector2Int targetResolution;

	public Camera mainCamera;
	public Camera renderCamera;
	public Transform renderQuad;
	public Material renderMaterial;

	private RenderTexture renderTexture;
	private int lastScreenWidth = 0;
	private int lastScreenHeight = 0;

	private void Update()
	{
		if (lastScreenWidth != Screen.width || lastScreenHeight != Screen.height)
		{
			lastScreenWidth = Screen.width;
			lastScreenHeight = Screen.height;
			OnScreenSizeChanged();
		}
	}

	private void OnScreenSizeChanged()
	{
		RefreshPixelPerfect();
	}

	private void RefreshPixelPerfect()
	{
		Debug.Log($"Refreshing camera");
		renderTexture = new RenderTexture(targetResolution.x, targetResolution.y, 24);
		renderTexture.filterMode = FilterMode.Point;

		mainCamera.targetTexture = renderTexture;
		renderMaterial.mainTexture = renderTexture;

		float pixelSize = 1f / PPU;
		float zoomedFloatSize = pixelSize / zoomScale;
		float ortographicSize = Screen.height / 2f * zoomedFloatSize;

		mainCamera.orthographicSize = renderCamera.orthographicSize = ortographicSize;
		renderQuad.localScale = new Vector3(Screen.width * zoomedFloatSize, Screen.height * zoomedFloatSize, 1f);
	}
}
