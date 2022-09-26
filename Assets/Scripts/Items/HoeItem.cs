using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "New Item", menuName = "Game/Hoe Item")]
public class HoeItem : Item, IUsable
{
    [SerializeField] private Tile _tileToSet;

    public void Use()
    {
        var worldPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        var grid = GameObject.Find("Ground").GetComponent<Tilemap>();
        var tilePos = grid.WorldToCell(worldPos);

		if (grid.GetTile(tilePos) != null)
        {
            grid.SetTile(tilePos, _tileToSet);
        }
	}
}
