using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
	[SerializeField] private GameObject lifeText;

    private void OnEnable() => GameEvents.PlayerLifeChanged += RefreshLifeText;
    private void OnDisable() => GameEvents.PlayerLifeChanged -= RefreshLifeText;

    public void RefreshLifeText(int newLifes)
	{
		lifeText.SetTextOutline(newLifes.ToString());
	}
}
