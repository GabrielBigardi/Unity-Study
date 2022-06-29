using GabrielBigardi.SpriteAnimator.Runtime;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	private PlayerCore _playerCore;
	public Vector2 PlayerInput;
	public SpriteAnimator SpriteAnimator;

    private void Start()
    {
		_playerCore = GetComponent<PlayerCore>();
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.TryGetComponent(out ICollectable collectable))
		{
			collectable.Collect();
			Destroy(collision.gameObject);
		}
	}
}
