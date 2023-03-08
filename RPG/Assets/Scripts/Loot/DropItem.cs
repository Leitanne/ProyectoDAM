using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DropItem
{
    [Header("Info")]
    public string Name;
    public InventoryItem Item;
    public int Amount;

    [Header("Drop")]
    [Range(0, 100)]public float DropChance;

    public bool PickedItem { get; set; }
}
