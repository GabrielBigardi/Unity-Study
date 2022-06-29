using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeCollectable : MonoBehaviour, ICollectable
{
    public void Collect()
	{
        GameEvents.PlayerCollectedItem?.Invoke(this);
	}
}
