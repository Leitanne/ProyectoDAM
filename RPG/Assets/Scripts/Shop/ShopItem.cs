using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    [Header("Config")] 
    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemCost;
    [SerializeField] private TextMeshProUGUI itemToBuy;

    public ItemSell ItemCharged { get; private set; }

    private int amount;
    private int inicialCost;
    private int currentCost;

    private void Update()
    {
        itemToBuy.text = amount.ToString();
        itemCost.text = currentCost.ToString();
    }

    public void ItemSellConfiguration(ItemSell sellItem)
    {
        ItemCharged = sellItem;
        itemIcon.sprite = sellItem.Item.Icon;
        itemName.text = sellItem.Item.Name;
        itemCost.text = sellItem.Cost.ToString();
        amount = 1;
        inicialCost = sellItem.Cost;
        currentCost = sellItem.Cost;
    }

    public void BuyItem()
    {
        if (CoinManager.Instance.TotalCoins >= currentCost)
        {
            Inventory.Instance.AddItem(ItemCharged.Item, amount);
            CoinManager.Instance.RemoveCoins(currentCost);
            amount = 1;
            currentCost = inicialCost;
        }
    }
    
    public void AddItemToBuy()
    {
        int costOfBuy = inicialCost * (amount + 1);
        if (CoinManager.Instance.TotalCoins >= costOfBuy)
        {
            amount++;
            currentCost = inicialCost * amount;
        }
    }

    public void RemoveItemToBuy()
    {
        if (amount == 1)
        {
            return;
        }

        amount--;
        currentCost = inicialCost * amount;
    }
}
