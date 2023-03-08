using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Weapon")]
public class WeaponItem : InventoryItem
{
    [Header("Arma")] 
    public Weapon Weapon;

    public override bool EquipItem()
    {
        if (WeaponContainer.Instance.EquipedWeapon != null)
        {
            return false;
        }
        
        WeaponContainer.Instance.EquipWeapon(this);
        return true;
    }

    public override bool RemoveItem()
    {
        if (WeaponContainer.Instance.EquipedWeapon == null)
        {
            return false;
        }
        
        WeaponContainer.Instance.RemoveWeapon();
        return true;
    }
}
