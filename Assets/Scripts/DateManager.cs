using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DateManager : MonoBehaviour
{
	private int _realCurrentDay = 1;
	private int _currentDay = 1;
	private int _currentMonth = 1;
	private int _currentYear = 1;

	public static event Action<int> GameDateDayChanged;
	public static event Action<int> GameDateMonthChanged;
	public static event Action<int> GameDateYearChanged;

	public static event Action<int, int, int, int> GameDateChanged;

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
		_realCurrentDay++;
		_currentDay++;
		if (_currentDay > 30)
		{
			_currentDay = 1;
			IncreaseMonth();
		}
		GameDateDayChanged?.Invoke(_currentDay);
		GameDateChanged?.Invoke(_realCurrentDay, _currentDay, _currentMonth, _currentYear);
	}

	private void IncreaseMonth()
	{
		_currentMonth++;
		if (_currentMonth > 12)
		{
			_currentMonth = 1;
			IncreaseYear();
		}
		GameDateMonthChanged?.Invoke(_currentMonth);
	}

	private void IncreaseYear()
	{
		_currentYear++;
		GameDateYearChanged?.Invoke(_currentYear);
	}
}
