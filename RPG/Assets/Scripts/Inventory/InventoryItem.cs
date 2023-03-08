using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum itemType
{
    Weapons,
    Potions,
    Scrolls,
    Ingredients,
    Treasures
}

public class InventoryItem : ScriptableObject
{
    [Header("Parametros")]
    public string ID;
    public string Name;
    public Sprite Icon;
    [TextArea] public string Description;

    [Header("Informacion")]
    public itemType type;
    public bool consumable;
    public bool canBeStacked;
    public int maxStack;

    [HideInInspector] public int amount;

    public InventoryItem CopyItem()
    {
        InventoryItem newInstance = Instantiate(this);
        return newInstance;
    }

    public virtual bool UseItem()
    {
        return true;
    }

    public virtual bool EquipItem()
    {
        return true;
    }

    public virtual bool RemoveItem()
    {
        return true;
    }
}
