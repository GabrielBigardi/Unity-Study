using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeCollectable : MonoBehaviour, ICollectable
{
	public static event Action Collected;

    public void Collect()
	{
        GameEvents.PlayerCollectedItem?.Invoke(this);
		Collected?.Invoke();
	}
}
