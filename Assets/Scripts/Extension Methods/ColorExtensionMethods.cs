// GetColor By FioteBearDev: www.twitch.tv/fiotebeardev

using System.Collections.Generic;
using UnityEngine;

public static class ColorExtensionMethods
{
	static Dictionary<string, Color> cachedColors = new Dictionary<string, Color>();

	/// <summary>
	/// Returns a "Color" given a hex color, ex: "#FF0000".
	/// </summary>
	/// <param name="string">The hex color to parse (ex: #FF0000)</param>
	/// <returns>New Color</returns>
	public static Color FromHex(this Color thisColor, string hex) {
		if (cachedColors.ContainsKey(hex)) return cachedColors[hex];
		Color color;
		ColorUtility.TryParseHtmlString(hex, out color);
		cachedColors[hex] = color;
		return color;
	}
}
