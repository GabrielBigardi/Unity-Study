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
        TimeManager.GameTimeChanged += OnGameTimeChanged;
    }

    private void OnDisable()
    {
        TimeManager.GameTimeChanged -= OnGameTimeChanged;
    }

    private void OnGameTimeChanged(int hour, int minutes, int seconds)
    {
        var blabla = _componentToEnable.GetType();

        if (hour == _timeToEnable.x)
            _componentToEnable.SetActive(true);

		if (hour == _timeToDisable.x)
			_componentToEnable.SetActive(false);
	}
}
