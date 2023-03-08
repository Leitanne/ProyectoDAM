using System.Collections;
using System.Collections.Generic;
using System;
using UnityEditor.Search;
using UnityEngine;

public class PickUpItems : MonoBehaviour
{
    [Header("Configuracion")]
    [SerializeField] private InventoryItem reference;
    [SerializeField] private int amountToAdd;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Inventory.Instance.AddItem(reference, amountToAdd);
            Destroy(gameObject);
        }
    }
}
