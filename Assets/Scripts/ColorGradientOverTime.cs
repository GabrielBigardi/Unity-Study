using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using DG.Tweening;

public enum TimeListenType
{
    Hour,
    Minute,
    Seconds
}

public class ColorGradientOverTime : MonoBehaviour
{
    [SerializeField] private TimeListenType _timeListenType;
    [SerializeField] private Gradient _dayGradient;
    [SerializeField] private Light2D _sunLight2D;
    [SerializeField] private float _lerpSpeed;

	private void OnEnable()
    {
        TimeManager.GameTimeHourChanged += OnHourChanged;
		TimeManager.GameTimeSet += OnTimeSet;
    }

    private void OnDisable()
    {
        TimeManager.GameTimeHourChanged -= OnHourChanged;
		TimeManager.GameTimeSet -= OnTimeSet;
	}

	private void OnHourChanged(int hour, int minutes, int seconds) => RefreshLight(hour, true);
	private void OnTimeSet(int hour, int minutes, int seconds) => RefreshLight(hour, false);

    private void RefreshLight(int whatToMap, bool lerp)
    {
        float mappedThing = 0f;

        switch (_timeListenType)
        {
            case TimeListenType.Hour:
				mappedThing = Helpers.Map(whatToMap, 0, 23, 0, 1);
				break;
            case TimeListenType.Minute:
				mappedThing = Helpers.Map(whatToMap, 0, 59, 0, 1);
                break;
			case TimeListenType.Seconds:
				mappedThing = Helpers.Map(whatToMap, 0, 59, 0, 1);
                break;
		}

        if (lerp)
            _sunLight2D.DOColor(_dayGradient.Evaluate(mappedThing), _lerpSpeed);
        else
            _sunLight2D.color = _dayGradient.Evaluate(mappedThing);
	}
}
