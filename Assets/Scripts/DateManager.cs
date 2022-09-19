using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DateManager : MonoBehaviour
{
	public int CurrentDay = 1;
	public int CurrentMonth = 1;
	public int CurrentYear = 1;

	public static event Action<int> GameDateDayChanged;
	public static event Action<int> GameDateMonthChanged;
	public static event Action<int> GameDateYearChanged;

	public static event Action<int, int, int> GameDateChanged;

	private void OnEnable()
	{
		TimeManager.GameTimeHourChanged += OnGameTimeHourChanged;
	}

	private void OnDisable()
	{
		TimeManager.GameTimeHourChanged -= OnGameTimeHourChanged;
	}

	private void OnGameTimeHourChanged(int hour, int minutes, int seconds)
	{
		if (hour != 0) return;

		IncreaseDay();
	}

	private void IncreaseDay()
	{
		CurrentDay++;
		if (CurrentDay > 30)
		{
			CurrentDay = 1;
			IncreaseMonth();
		}
		GameDateDayChanged?.Invoke(CurrentDay);
		GameDateChanged?.Invoke(CurrentDay, CurrentMonth, CurrentYear);
	}

	private void IncreaseMonth()
	{
		CurrentMonth++;
		if (CurrentMonth > 12)
		{
			CurrentMonth = 1;
			IncreaseYear();
		}
		GameDateMonthChanged?.Invoke(CurrentMonth);
	}

	private void IncreaseYear()
	{
		CurrentYear++;
		GameDateYearChanged?.Invoke(CurrentYear);
	}
}
