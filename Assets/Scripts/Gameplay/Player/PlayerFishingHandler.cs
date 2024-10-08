using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Random = UnityEngine.Random;
using UnityEngine.InputSystem;
using System.Runtime.InteropServices.ComTypes;

public class PlayerFishingHandler : MonoBehaviour
{
	public List<FishItem> PossibleFishes;
    public GameObject FishingPanel;
	public SpriteRenderer FishingCatchBar;
	public SpriteRenderer FishingProgressBar;
	public SpriteRenderer MovingFishSprite;
	public float FishingProgress;
	public int FishingBarPixels;
	bool IsFishing = false;
	public bool IsBarTouchingFish = false;
	public float ProgressBarSpeed;

	[Header("Popup")]
	public GameObject FishPopupPanel;
	public TMP_Text FishName;
	public SpriteRenderer FishIcon;
	public TMP_Text FishLength;

	public static event Action<List<FishItem>> PlayerFishingStarted;
	public static event Action PlayerFishingEnded;
	public static event Action<FishItem> PlayerCatchedFish;

	bool mouseLeftButton = false;

	private void OnEnable()
	{
		PoolInteractable.Interacted += OnPoolInteractableInteracted;
		FishCollisionHandler.FishingBarCollisionWithFish += OnFishingBarCollisionWithFish;
		PlayerInputHandler.PlayerMouseInput += OnPlayerMouseInput;
	}

	private void OnDisable()
	{
		PoolInteractable.Interacted -= OnPoolInteractableInteracted;
		FishCollisionHandler.FishingBarCollisionWithFish -= OnFishingBarCollisionWithFish;
		PlayerInputHandler.PlayerMouseInput -= OnPlayerMouseInput;
	}

	private void Update()
	{
		if (!IsFishing)
			return;

		FishingBarMovement();
		UpdateProgressBar();
	}

	private void OnPlayerMouseInput(InputAction.CallbackContext ctx)
	{
		if (ctx.started)
			mouseLeftButton = true;

		if (ctx.canceled)
			mouseLeftButton = false;
	}

	private void OnFishingBarCollisionWithFish(bool colliding)
	{
		IsBarTouchingFish = colliding;
	}

	private void FishingBarMovement()
	{
		float barVelocity = 2f;

		if ((FishingCatchBar.transform.localPosition.y - (FishingCatchBar.size.y / 2)) < -(0.0625f * 28) && !mouseLeftButton)
			barVelocity = 0f;
		else if ((FishingCatchBar.transform.localPosition.y + (FishingCatchBar.size.y / 2)) > (0.0625f * 28) && mouseLeftButton)
			barVelocity = 0f;

		FishingCatchBar.transform.localPosition += new Vector3(0f, mouseLeftButton ? barVelocity * Time.deltaTime : -barVelocity * Time.deltaTime, 0f);
	}

	private void UpdateProgressBar()
	{
		if (IsBarTouchingFish)
		{
			FishingProgress += ProgressBarSpeed * Time.deltaTime;
		}
		else
		{
			FishingProgress -= (ProgressBarSpeed * 0.7f) * Time.deltaTime;
		}

		if(FishingProgress >= 1f)
		{
			FishingProgress = 1f;
			CatchRandomFish();
			EndFishing();
		}
		else if(FishingProgress <= 0f)
		{
			FishingProgress = 0f;
			EndFishing();
		}

		//FishingProgressBar.transform.localScale = new Vector3(1f, FishingProgress, 1f);
		FishingProgressBar.transform.localPosition = new Vector3(FishingProgressBar.transform.localPosition.x, Helpers.Map(FishingProgress, 0f, 1f, -(0.0625f * 58), 0f));
	}

	private void CatchRandomFish()
	{
		StartCoroutine(FishPopup_CR());
	}

	IEnumerator FishPopup_CR()
	{
		FishItem randomFish = PossibleFishes.RandomElement();
		Debug.Log($"You got a {randomFish.Name}");
		FishName.SetText(randomFish.Name);
		FishIcon.sprite = randomFish.Sprite;
		FishLength.SetText($"{Random.Range(randomFish.InchesMinSize, randomFish.InchesMaxSize)} in.");
		FishPopupPanel.SetActive(true);
		PlayerCatchedFish?.Invoke(randomFish);
		yield return new WaitForSeconds(2f);
		FishPopupPanel.SetActive(false);
	}

	public void OnPoolInteractableInteracted(List<FishItem> possibleFishes)
	{
		FishPopupPanel.SetActive(false);
		PossibleFishes = possibleFishes;
		FishingProgress = 0.3f;
		FishingPanel.SetActive(true);
		FishingCatchBar.transform.localPosition = new Vector3(FishingCatchBar.transform.localPosition.x, 0f, 0f);
		FishingCatchBar.size = new Vector2(FishingCatchBar.size.x, FishingBarPixels * 0.0625f);
		FishingCatchBar.GetComponent<BoxCollider2D>().size = FishingCatchBar.size;
		FishingProgressBar.size = new Vector2(FishingProgressBar.size.x, FishingProgress);
		MovingFishSprite.transform.localPosition = new Vector3(MovingFishSprite.transform.localPosition.x, 0f, 0f);
		IsFishing = true;

		PlayerFishingStarted?.Invoke(possibleFishes);
	}

	public void EndFishing()
	{
		FishingPanel.SetActive(false);
		IsFishing = false;

		PlayerFishingEnded?.Invoke();
	}
}
