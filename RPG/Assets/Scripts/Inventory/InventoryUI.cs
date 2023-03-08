using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public class InventoryUI : Singleton<InventoryUI>
{
    [Header("Panel Inventario Descripcion")]

    [SerializeField] private GameObject inventoryDescriptionPanel;
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemDescription;

    [SerializeField] private InventorySlot slotPrefab;
    [SerializeField] private Transform container;

    public int IndexMovingSlot { get; private set; }
    public InventorySlot selectedSlot { get; private set; }
    private List<InventorySlot> slots = new List<InventorySlot>();
    
    private void Start()
    {
        StartInventory();
        IndexMovingSlot = -1;
    }

    private void Update()
    {
        UpdateSelectedSlot();
        if(Input.GetKeyDown(KeyCode.M))
        {
            if(selectedSlot != null)
            {
                IndexMovingSlot = selectedSlot.Index;
            }
        }
    }

    private void StartInventory()
    {
        for (int i = 0; i < Inventory.Instance.NumberOfSlots; i++)
        {
            InventorySlot newSlot = Instantiate(slotPrefab, container);
            newSlot.Index = i;
            slots.Add(newSlot);
        }
    }

    private void UpdateSelectedSlot()
    {
        GameObject selectedGameObject = EventSystem.current.currentSelectedGameObject;
        
        if (selectedGameObject == null)
        {
            return;
        }

        InventorySlot slot = selectedGameObject.GetComponent<InventorySlot>();
        if(slot != null)
        {
            selectedSlot = slot;
        }
    }

    public void DrawItem(InventoryItem item, int amount, int index)
    {
        InventorySlot slot = slots[index];
        if(item != null)
        {
            slot.ActivateSlotUI(true);
            slot.UpdateSlotUI(item, amount);
        }
        else
        {
            slot.ActivateSlotUI(false);
        }
    }

    private void updateDescription(int index)
    {
        if (Inventory.Instance.InventoryItems[index] != null)
        {
            icon.sprite = Inventory.Instance.InventoryItems[index].Icon;
            itemName.text = Inventory.Instance.InventoryItems[index].Name;
            itemDescription.text = Inventory.Instance.InventoryItems[index].Description;
            inventoryDescriptionPanel.SetActive(true);
        }
        else
        {
            inventoryDescriptionPanel.SetActive(false);
        }
    }

    #region Events
    private void SlotInteractionResponse(TypeOfUse use, int index)
    {
        if (use == TypeOfUse.Click)
        {
            updateDescription(index);
        }
    }

    private void OnEnable()
    {
        InventorySlot.eventSlotInteraction += SlotInteractionResponse;
    }

    private void OnDisable()
    {
        InventorySlot.eventSlotInteraction -= SlotInteractionResponse;
    }

    public void UseItem()
    {
        if (selectedSlot != null)
        {
            selectedSlot.SlotUseItem();
            selectedSlot.selectSlot();
        }
    }

    public void EquipItem()
    {
        if (selectedSlot != null)
        {
            selectedSlot.SlotEquipItem();
            selectedSlot.selectSlot();
        }
    }

    public void RemoveItem()
    {
        if (selectedSlot != null)
        {
            selectedSlot.SlotRemoveItem();
            selectedSlot.selectSlot();
        }
    }
    #endregion
}
