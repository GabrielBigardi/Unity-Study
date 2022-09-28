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

	public bool Use(PlayerCore playerCore)
    {
        var mouseWorldPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
		mouseWorldPos.z = 0f;
		var grid = GameObject.Find("Ground").GetComponent<Tilemap>();
        var tilePos = grid.WorldToCell(mouseWorldPos);
        var tileAtPos = grid.GetTile(tilePos);

		var playerCellPos = grid.WorldToCell(playerCore.transform.position);
		var distancePlayerAndClick = tilePos - playerCellPos;
		if(Mathf.Abs(distancePlayerAndClick.x) > 1f || Mathf.Abs(distancePlayerAndClick.y) > 1f)
		{
			var playerPos = playerCore.transform.position;
			var roundedMouseDistance = Vector3Int.RoundToInt((mouseWorldPos - playerPos).normalized);
			var tilePos2 = grid.WorldToCell(playerCore.transform.position + (Vector3)roundedMouseDistance);
			var tileAtPos2 = grid.GetTile(tilePos2);

			if (TryToSetTile(grid, tileAtPos2, tilePos2))
				return true;
		}
		else
		{
			if (TryToSetTile(grid, tileAtPos, tilePos))
				return true;
		}

        return false;
	}

	private bool TryToSetTile(Tilemap grid, TileBase tileAtPos, Vector3Int tilePos)
	{
		if (tileAtPos != null && _tileToCheck.Contains(tileAtPos))
		{
			grid.SetTile(tilePos, _tileToSet);
			return true;
		}
		return false;
	}
}
