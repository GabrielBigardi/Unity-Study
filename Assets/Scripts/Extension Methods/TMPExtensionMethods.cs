using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public static class TMPExtensionMethods
{
    public static void SetTextOutline(this GameObject tmpText, string text)
    {
		foreach (var outline in tmpText.GetComponentsInChildren<TMP_Text>())
		{
			outline.SetText(text);
		}
    }
}
