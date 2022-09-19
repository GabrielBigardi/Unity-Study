using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public static class DOTweenExtensionMethods
{
	public static Tweener DOColor(this Light2D target, Color endValue, float duration)
	{
		return DOTween.To(() => target.color, x => target.color = x, endValue, duration).SetTarget(target);
	}
}
