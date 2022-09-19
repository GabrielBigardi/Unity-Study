using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnabledAtSpecificTime : MonoBehaviour
{
    [SerializeField] private GameObject _componentToEnable;
    [SerializeField] private Vector3Int _timeToEnable;
    [SerializeField] private Vector3Int _timeToDisable;

	private void OnEnable()
    {
        TimeManager.GameTimeHourChanged += OnHourChanged;
        TimeManager.GameTimeChanged += OnTimeChanged;
        TimeManager.GameTimeSet += OnTimeSet;
	}

    private void OnDisable()
    {
        TimeManager.GameTimeHourChanged += OnHourChanged;
        TimeManager.GameTimeChanged -= OnTimeChanged;
		TimeManager.GameTimeSet -= OnTimeSet;
	}

	private void OnHourChanged(int hour, int minutes, int seconds) => CheckTime(hour, 0, 0); 
	private void OnTimeChanged(int hour, int minutes, int seconds) => CheckTime(hour, 0, 0); 
	private void OnTimeSet(int hour, int minutes, int seconds) => CheckTime(hour, 0, 0); 

    private void CheckTime(int hour, int minutes, int seconds)
    {
        var blabla = _componentToEnable.GetType();

        if (hour == _timeToEnable.x)
            _componentToEnable.SetActive(true);

		if (hour == _timeToDisable.x)
			_componentToEnable.SetActive(false);
	}
}
