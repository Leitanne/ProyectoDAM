using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : Singleton<Inventory>
{
    [Header("Items")]
    [SerializeField] private InventoryItem[] inventoryItems;
    [SerializeField] private Character character;
    [SerializeField] private int numberOfSlots;

    public Character Character => character;
    public int NumberOfSlots => numberOfSlots;
    public InventoryItem[] InventoryItems => inventoryItems;

    private void Start()
    {
        inventoryItems = new InventoryItem[numberOfSlots];
    }

    public void AddItem(InventoryItem item, int amount)
    {
        if(item == null || amount <= 0)
        {
            return;
        }

        //Item preexistente
        List<int> index = VerifyItem(item.ID);
        if (item.canBeStacked)
        {
            for (int i = 0; i < index.Count; i++)
            {
                if (inventoryItems[index[i]].amount < item.maxStack)
                {
                    inventoryItems[index[i]].amount += amount;
                    if(inventoryItems[index[i]].amount > item.maxStack)
                    {
                        int diference = inventoryItems[index[i]].amount - item.maxStack;
                        inventoryItems[index[i]].amount = item.maxStack;
                        AddItem(item, diference);
                    }

                    InventoryUI.Instance.DrawItem(item, inventoryItems[index[i]].amount, index[i]);
                    return;
                }
            }
        }

        //No hay item preexistente
        if (amount > item.maxStack)
        {
            AddItemInAvaliableSlot(item, item.maxStack);
            amount -= item.maxStack;
            AddItem(item, amount);
        }
        else
        {
            AddItemInAvaliableSlot(item, amount);
        }
    }

    //Verifica todos los stacks del item en cuestion y en que slot estan
    private List<int> VerifyItem(string id)
    {
        List<int> itemIndex = new List<int>();

        for (int i = 0; i < inventoryItems.Length; i++)
        {
            if (inventoryItems[i] != null) 
            {
                if (inventoryItems[i].ID == id)
                {
                    itemIndex.Add(i);
                }
            }
        }

        return itemIndex;
    }

    private void AddItemInAvaliableSlot(InventoryItem item, int amount)
    {
        for (int i = 0; i < inventoryItems.Length; i++)
        {
            if (inventoryItems[i] == null)
            {
                inventoryItems[i] = item.CopyItem();
                inventoryItems[i].amount = amount;
                InventoryUI.Instance.DrawItem(item, amount, i);
                return;
            }
        }
    }

    private void DeleteItem(int index)
    {
        inventoryItems[index].amount--;

        if(inventoryItems[index].amount <= 0)
        {
            inventoryItems[index].amount = 0;
            inventoryItems[index] = null;
            InventoryUI.Instance.DrawItem(null, 0, index);
        }
        else
        {
            InventoryUI.Instance.DrawItem(inventoryItems[index], inventoryItems[index].amount, index);
        }
    }

    public void MoveItem(int startIndex, int endIndex)
    {
        if (inventoryItems[startIndex] == null || inventoryItems[endIndex] != null)
        {
            return;
        }

        //Copy item
        InventoryItem itemToMove = inventoryItems[startIndex].CopyItem();
        inventoryItems[endIndex] = itemToMove;
        InventoryUI.Instance.DrawItem(itemToMove, itemToMove.amount, endIndex);

        //Delete item from prior slot
        inventoryItems[startIndex] = null;
        InventoryUI.Instance.DrawItem(null, 0, startIndex);
    }


    private void UseItem(int index)
    {
        if (inventoryItems[index] == null)
        {
            return;
        }

        if (inventoryItems[index].UseItem())
        {
            DeleteItem(index);
        }
    }

    private void EquipItem(int index)
    {
        if (inventoryItems[index] == null)
        {
            return;
        }

        if(inventoryItems[index].type != itemType.Weapons)
        {
            return;
        }

        inventoryItems[index].EquipItem();
    }

    private void RemoveItem(int index)
    {
        if (inventoryItems[index] == null)
        {
            return;
        }

        if (inventoryItems[index].type != itemType.Weapons)
        {
            return;
        }

        inventoryItems[index].RemoveItem();
    }
    #region Events

    private void slotInteractionResponse(TypeOfUse type, int index)
    {
        switch(type)
        {
            case TypeOfUse.Use:
                UseItem(index);
                break;
            case TypeOfUse.Equip:
                EquipItem(index);
                break;
            case TypeOfUse.Remove:
                RemoveItem(index);
                break;
        }
    }

    private void OnEnable()
    {
        InventorySlot.eventSlotInteraction += slotInteractionResponse;
    }

    private void OnDisable()
    {
        InventorySlot.eventSlotInteraction -= slotInteractionResponse;
    }
    #endregion
}
