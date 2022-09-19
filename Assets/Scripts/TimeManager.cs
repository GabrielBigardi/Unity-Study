using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public int CurrentHour => (int)(_gameTimer / 3600) % 24;
	public int CurrentMinute => (int)(_gameTimer / 60) % 60;
    public int CurrentSecond => (int)(_gameTimer % 60);

    private float _gameTimer = 0f;

    public static event Action<int> GameTimeSecondChanged;
    public static event Action<int> GameTimeMinuteChanged;
    public static event Action<int> GameTimeHourChanged;

    public static event Action<int,int,int> GameTimeChanged;

	private void Update()
    {
        var previousSecond = CurrentSecond;
        var previousMinute = CurrentMinute;
        var previousHour = CurrentHour;

        _gameTimer += Time.deltaTime * 2000f;

        if (previousSecond != CurrentSecond)
        {
			GameTimeSecondChanged?.Invoke(CurrentSecond);
            GameTimeChanged?.Invoke(CurrentHour, CurrentMinute, CurrentSecond);
		}
		if (previousMinute != CurrentMinute)
        {
			GameTimeMinuteChanged?.Invoke(CurrentMinute);
            GameTimeChanged?.Invoke(CurrentHour, CurrentMinute, CurrentSecond);
		}
		if (previousHour != CurrentHour)
        {
			GameTimeHourChanged?.Invoke(CurrentHour);
            GameTimeChanged?.Invoke(CurrentHour, CurrentMinute, CurrentSecond);
		}
	}

    public void SetGameTime(int hour, int minute, int second)
    {
        _gameTimer = (hour * 3600) + (minute * 60) + (second);
        GameTimeChanged?.Invoke(CurrentHour, CurrentMinute, CurrentSecond);
	}
}