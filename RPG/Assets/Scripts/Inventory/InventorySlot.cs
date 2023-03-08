using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum TypeOfUse
{
    Click,
    Use,
    Equip,
    Remove
}

public class InventorySlot : MonoBehaviour
{
    public static Action<TypeOfUse, int> eventSlotInteraction;

    [SerializeField] private Image itemIcon;
    [SerializeField] private GameObject amountBackground;
    [SerializeField] private TextMeshProUGUI amountTMP;
    public int Index { get; set; }
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }
    public void UpdateSlotUI(InventoryItem item, int amount)
    {
        itemIcon.sprite = item.Icon;
        amountTMP.text = amount.ToString();
    }

    public void ActivateSlotUI(bool state)
    {
        itemIcon.gameObject.SetActive(state);
        amountBackground.SetActive(state);
    }

    public void selectSlot()
    {
        _button.Select();
    }
    public void ClickSlot()
    {
        eventSlotInteraction?.Invoke(TypeOfUse.Click, Index);

        //Move item
        if (InventoryUI.Instance.IndexMovingSlot != -1)
        {
            if(InventoryUI.Instance.IndexMovingSlot != Index)
            {
                Inventory.Instance.MoveItem(InventoryUI.Instance.IndexMovingSlot, Index);
            }
        }
    }

    public void SlotUseItem()
    {
        if (Inventory.Instance.InventoryItems[Index] != null)
        {
            eventSlotInteraction?.Invoke(TypeOfUse.Use, Index);
        }
    }

    public void SlotEquipItem()
    {
        if (Inventory.Instance.InventoryItems[Index] != null)
        {
            eventSlotInteraction?.Invoke(TypeOfUse.Equip, Index);
        }
    }

    public void SlotRemoveItem()
    {
        if (Inventory.Instance.InventoryItems[Index] != null)
        {
            eventSlotInteraction?.Invoke(TypeOfUse.Remove, Index);
        }
    }
}
