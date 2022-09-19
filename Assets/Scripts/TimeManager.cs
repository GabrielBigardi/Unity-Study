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

    public static event Action<int,int,int> GameTimeSecondChanged;
    public static event Action<int,int,int> GameTimeMinuteChanged;
    public static event Action<int,int,int> GameTimeHourChanged;

    public static event Action<int,int,int> GameTimeChanged;
    public static event Action<int,int,int> GameTimeSet;

    [SerializeField] private float _timeMultiplier;

    private void Start()
    {
        SetGameTime(12, 0, 0);
    }

    private void Update()
    {
        var previousSecond = CurrentSecond;
        var previousMinute = CurrentMinute;
        var previousHour = CurrentHour;

        _gameTimer += Time.deltaTime * _timeMultiplier;

        if (previousSecond != CurrentSecond)
        {
			GameTimeSecondChanged?.Invoke(CurrentHour, CurrentMinute, CurrentSecond);
			GameTimeChanged?.Invoke(CurrentHour, CurrentMinute, CurrentSecond);
		}
		if (previousMinute != CurrentMinute)
        {
			GameTimeMinuteChanged?.Invoke(CurrentHour, CurrentMinute, CurrentSecond);
		}
		if (previousHour != CurrentHour)
        {
			GameTimeHourChanged?.Invoke(CurrentHour, CurrentMinute, CurrentSecond);
		}
	}

    public void SetGameTime(int hour, int minute, int second)
    {
        _gameTimer = (hour * 3600) + (minute * 60) + (second);
		GameTimeChanged?.Invoke(CurrentHour, CurrentMinute, CurrentSecond);
		GameTimeSet?.Invoke(CurrentHour, CurrentMinute, CurrentSecond);
	}
}
