using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LootButton : MonoBehaviour
{
    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI itemName;

    public DropItem ItemToPickUp { get; set; }
    
    public void ConfigureLootItem(DropItem dropItem)
    {
        ItemToPickUp = dropItem;
        itemIcon.sprite = dropItem.Item.Icon;
        itemName.text = $"{dropItem.Item.Name} x{dropItem.Amount}";
    }

    public void PickUpItem()
    {
        if (ItemToPickUp == null)
        {
            return;
        }
        
        Inventory.Instance.AddItem(ItemToPickUp.Item, ItemToPickUp.Amount);
        ItemToPickUp.PickedItem = true;
        Destroy(gameObject);
    }
}
