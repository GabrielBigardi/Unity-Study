using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "New Item", menuName = "Game/Seed Item")]
public class SeedItem : Item, IUsable
{
	[SerializeField] private Tile[] _tileToCheck;
	[SerializeField] private Tile[] _tileToSet;
	public bool RemoveOnUse => true;

	public bool Use(PlayerCore playerCore)
	{
		var worldPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
		var grid = GameObject.Find("Ground").GetComponent<Tilemap>();
		var tilePos = grid.WorldToCell(worldPos);
		var tileAtPos = grid.GetTile(tilePos);

		if (tileAtPos != null && _tileToCheck.Contains(tileAtPos))
		{
			var index = Array.FindIndex(_tileToCheck, x => x == tileAtPos);
			grid.SetTile(tilePos, _tileToSet[index]);
			return true;
		}

		return false;
	}
}
