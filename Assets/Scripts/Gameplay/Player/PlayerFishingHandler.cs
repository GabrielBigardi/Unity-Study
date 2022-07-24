using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerFishingHandler : MonoBehaviour
{
	public List<Fish> PossibleFishes;
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

	private void OnEnable()
	{
		GameEvents.PlayerFishingStarted += OnPlayerFishingStarted;
		GameEvents.PlayerFishingEnded += OnPlayerFishingEnded;
	}

	private void OnDisable()
	{
		GameEvents.PlayerFishingStarted -= OnPlayerFishingStarted;
		GameEvents.PlayerFishingEnded -= OnPlayerFishingEnded;
	}

	private void Update()
	{
		if (!IsFishing)
			return;

		FishingBarMovement();
		UpdateProgressBar();
	}

	private void FishingBarMovement()
	{
		bool mouseLeftButton = GetComponent<PlayerCore>().PlayerInputHandler.MouseLeftButton;
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
			GameEvents.PlayerFishingEnded?.Invoke();
		}else if(FishingProgress <= 0f)
		{
			FishingProgress = 0f;
			GameEvents.PlayerFishingEnded?.Invoke();
		}

		FishingProgressBar.size = new Vector2(FishingProgressBar.size.x, (0.0625f * 60) * FishingProgress);
		FishingProgressBar.transform.localPosition = new Vector3(FishingProgressBar.transform.localPosition.x, (FishingProgressBar.size.y / 2) - (0.0625f * 30));
	}

	private void CatchRandomFish()
	{
		StartCoroutine(FishPopup_CR());
	}

	IEnumerator FishPopup_CR()
	{
		Fish randomFish = PossibleFishes.RandomElement();
		Debug.Log($"You got a {randomFish.Name}");
		FishName.SetText(randomFish.Name);
		FishLength.SetText($"{Random.Range(randomFish.InchesMinSize, randomFish.InchesMaxSize)} in.");
		FishPopupPanel.SetActive(true);
		GameEvents.PlayerItemAdded?.Invoke(randomFish);
		yield return new WaitForSeconds(2f);
		FishPopupPanel.SetActive(false);
	}

	public void OnPlayerFishingStarted(List<Fish> possibleFishes)
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
	}

	public void OnPlayerFishingEnded()
	{
		FishingPanel.SetActive(false);
		IsFishing = false;
		GetComponent<PlayerInteractionHandler>().CheckInteractableTiles(transform.position);
	}
}
