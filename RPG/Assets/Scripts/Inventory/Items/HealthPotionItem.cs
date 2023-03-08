using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Health Potion")]
public class HealthPotionItem : InventoryItem
{
    [Header("Pocion info")]
    public float healthRestore;

    public override bool UseItem()
    {
        if (Inventory.Instance.Character.characterHealth.canBeHealed)
        {
            Inventory.Instance.Character.characterHealth.restoreHealth(healthRestore);
            return true;
        }

        return false;
    }

    public override bool EquipItem()
    {
        return base.EquipItem();
    }

    public override bool RemoveItem()
    {
        return base.RemoveItem();
    }
}
