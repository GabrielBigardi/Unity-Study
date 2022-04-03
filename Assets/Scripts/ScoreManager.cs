using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : Singleton<ScoreManager>
{
	public TMPro.TMP_Text scoreText;

	private int _currentScore;
	public int CurrentScore
	{
		get
		{
			return _currentScore;
		}
		set
		{
			_currentScore = value;
			scoreText.SetText($"Score: {_currentScore}");
		}
	}

	public void AddScore()
	{
		CurrentScore++;
	}
}
