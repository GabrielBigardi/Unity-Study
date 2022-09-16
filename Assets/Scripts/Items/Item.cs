using System;
using UnityEditor;
using UnityEngine;

public class Item : ScriptableObject
{
    public string GUID;
    public string Name;
    public string Description;
    public Sprite Sprite;

    public Item()
	{
        GUID = Guid.NewGuid().ToString();
    }

    [ContextMenu("Regenerate GUID")]
    private void ResetGUID()
	{
        GUID = Guid.NewGuid().ToString();
    }
}