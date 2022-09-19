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

	private void OnEnable()
    {
        switch (_timeListenType)
        {
            case TimeListenType.Hour:
                TimeManager.GameTimeHourChanged += HandleTimeChange;
                break;
            case TimeListenType.Minute:
                TimeManager.GameTimeMinuteChanged += HandleTimeChange;
                break;
			case TimeListenType.Seconds:
                TimeManager.GameTimeSecondChanged += HandleTimeChange;
                break;
        }
    }

    private void OnDisable()
    {
		switch (_timeListenType)
		{
			case TimeListenType.Hour:
				TimeManager.GameTimeHourChanged -= HandleTimeChange;
				break;
			case TimeListenType.Minute:
				TimeManager.GameTimeMinuteChanged -= HandleTimeChange;
				break;
			case TimeListenType.Seconds:
				TimeManager.GameTimeSecondChanged -= HandleTimeChange;
				break;
		}
	}

    private void Start() => RefreshLight(0, false);

    private void HandleTimeChange(int whatChanged) => RefreshLight(whatChanged, true);

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
            _sunLight2D.DOColor(_dayGradient.Evaluate(mappedThing), 2f);
        else
            _sunLight2D.color = _dayGradient.Evaluate(mappedThing);
	}
}
