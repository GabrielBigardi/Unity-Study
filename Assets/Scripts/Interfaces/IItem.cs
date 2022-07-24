using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItem
{
    public string Name { get; }
    public string Description { get; }
    public Sprite Sprite { get; }
}
