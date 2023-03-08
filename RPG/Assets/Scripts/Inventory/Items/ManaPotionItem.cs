using UnityEngine;

[CreateAssetMenu(menuName = "Items/Mana Potion")]
public class ManaPotionItem : InventoryItem
{
    [Header("Pocion info")]
    public float manaRestore;

    public override bool UseItem()
    {
        if (Inventory.Instance.Character.characterMana.canRestoreMana)
        {
            Inventory.Instance.Character.characterMana.RestoreMana(manaRestore);
            return true;
        }

        return false;
    }
}
