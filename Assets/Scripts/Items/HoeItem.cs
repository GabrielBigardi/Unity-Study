using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "New Item", menuName = "Game/Hoe Item")]
public class HoeItem : Item, IUsable
{
	[SerializeField] private Tile[] _tileToCheck;
	[SerializeField] private Tile _tileToSet;
	public bool RemoveOnUse => false;

	public bool Use()
    {
        var worldPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        var grid = GameObject.Find("Ground").GetComponent<Tilemap>();
        var tilePos = grid.WorldToCell(worldPos);
        var tileAtPos = grid.GetTile(tilePos);

		if (tileAtPos != null && _tileToCheck.Contains(tileAtPos))
        {
            grid.SetTile(tilePos, _tileToSet);
            return true;
        }

        return false;
	}
}
