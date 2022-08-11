using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Game/Fish Item")]
public class FishItem : Item
{
	public int BasePrice;
	public int BaseExp;
	public int TimeRangeMin;
	public int TimeRangeMax;
	public int InchesMinSize;
	public int InchesMaxSize;
	public int Difficulty;
}