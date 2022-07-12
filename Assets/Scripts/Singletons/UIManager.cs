using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
	[SerializeField] private GameObject _lifeText;
	[SerializeField] private Image _fadeImage;

	private void OnEnable()
    {
		GameEvents.PlayerLifeChanged += RefreshLifeText;
		GameEvents.PlayerInteractionCompleted += HandlePlayerInteractions;
	}

    private void OnDisable()
    {
		GameEvents.PlayerLifeChanged -= RefreshLifeText;
		GameEvents.PlayerInteractionCompleted -= HandlePlayerInteractions;
	}

    private void Start()
    {
		_fadeImage.color = Color.black;
		_fadeImage.DOColor(Color.clear, 1f);

		Debug.Log(new Color().FromHex("#ff0000"));
	}

    public void RefreshLifeText(int newLifes)
	{
		_lifeText.SetTextOutline(newLifes.ToString());
	}

	private void HandlePlayerInteractions(IInteractable interactable)
	{
		if (interactable is HouseInteractable houseInteractable)
			EnterHouse();
	}

	public void EnterHouse()
    {
		var activeScene = SceneManager.GetActiveScene().buildIndex;
		_fadeImage.DOColor(Color.black, 1f).OnComplete(() => _fadeImage.DOColor(Color.clear, 1f));
    }
}
