using GabrielBigardi.SpriteAnimator.Runtime;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerCollisionSenses : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.TryGetComponent(out ICollectable collectable))
		{
			collectable.Collect();
			Destroy(collision.gameObject);
		}
	}
}
