using System;
using UnityEngine;

public class ManagerShop : MonoBehaviour
{
    [Header("Config")] 
    [SerializeField] private ShopItem itemShopPrefab;
    [SerializeField] private Transform containerPanel;

    [Header("Items")] 
    [SerializeField] private ItemSell[] avaliableItems;

    private void Start()
    {
        LoadItems();
    }

    private void LoadItems()
    {
        for (int i = 0; i < avaliableItems.Length; i++)
        {
            ShopItem shopItem = Instantiate(itemShopPrefab, containerPanel);
            shopItem.ItemSellConfiguration(avaliableItems[i]);
        }
    }
}