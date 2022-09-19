using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using DG.Tweening;

public class DayCycleManager : MonoBehaviour
{
    [SerializeField] private Gradient _dayGradient;
    [SerializeField] private Light2D _sunLight2D;

    private void OnEnable() => TimeManager.GameTimeHourChanged += OnGameTimeHourChanged;

    private void OnDisable() => TimeManager.GameTimeHourChanged -= OnGameTimeHourChanged;

    private void Start() => RefreshLight(0, false);

    private void OnGameTimeHourChanged(int hour) => RefreshLight(hour, true);

    private void RefreshLight(int hour, bool lerp)
    {
		float mappedHour = Helpers.Map(hour, 0, 24, 0, 1);

        if (lerp)
            _sunLight2D.DOColor(_dayGradient.Evaluate(mappedHour), 2f);
        else
            _sunLight2D.color = _dayGradient.Evaluate(mappedHour);
	}
}
