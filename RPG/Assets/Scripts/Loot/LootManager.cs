using UnityEngine;

public class LootManager : Singleton<LootManager>
{
    [Header("Config")]
    [SerializeField] private GameObject panelLoot;
    [SerializeField] private LootButton lootButtonPrefab;
    [SerializeField] private Transform lootContiner;

    
    public void ShowLoot(EnemyLoot enemyLoot)
    {
        panelLoot.SetActive(true);
        if (BusyContainer())
        {
            foreach (Transform child in lootContiner.transform)
            {
                Destroy(child.gameObject);
            }
        }
        
        for (int i = 0; i < enemyLoot.SelectedLoot.Count; i++)
        {
            LoadLootPanel(enemyLoot.SelectedLoot[i]);
        }
        
    }
    
    public void ClosePanel()
    {
        panelLoot.SetActive(false);
    }
    
    private void LoadLootPanel(DropItem dropItem)
    {
        if (dropItem.PickedItem)
        {
            return;
        }

        LootButton loot = Instantiate(lootButtonPrefab, lootContiner);
        loot.ConfigureLootItem(dropItem);
        loot.transform.SetParent(lootContiner);
    }

    private bool BusyContainer()
    {
        LootButton[] childs = lootContiner.GetComponentsInChildren<LootButton>();
        if (childs.Length > 0)
        {
            return true;
        }

        return false;
    }
}
