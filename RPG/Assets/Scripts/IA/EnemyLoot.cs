using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyLoot : MonoBehaviour
{
    [Header("Exp")] 
    [SerializeField] private float expWon;
    
    [Header("Loot")] 
    [SerializeField] private DropItem[] avaliableLoot;

    private List<DropItem> selectedLoot = new List<DropItem>();
    public List<DropItem> SelectedLoot => selectedLoot;
    public float ExpWon => expWon;

    private void Start()
    {
        SelectLoot();
    }

    private void SelectLoot()
    {
        foreach (DropItem item in avaliableLoot)
        {
            float probability = Random.Range(0, 100);
            if (probability <= item.DropChance)
            {
                selectedLoot.Add(item);
            }
        }
    }
}