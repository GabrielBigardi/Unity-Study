using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LifeManager : Singleton<LifeManager>
{
    public int CurrentLifes { get; private set; }

	[SerializeField] private GameObject lifeText;

	private void Start()
	{
		CurrentLifes = 1;
	}

	public void AddLifes(int amount)
	{
		CurrentLifes += amount;
		RefreshLifeText();
	}

	public void RemoveLifes(int amount)
	{
		CurrentLifes -= amount;
		RefreshLifeText();
	}

	public void RefreshLifeText()
	{
		lifeText.SetTextOutline(CurrentLifes.ToString());
	}
}
