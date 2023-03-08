using System;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    public static Action<EnemyInteraction> EventEnemySelected;
    public static Action EventObjectNoSelected;

    public EnemyInteraction SelectedEnemy { get; set; }
    
    private Camera camera;
    
    private void Start()
    {
        camera = Camera.main;
    }

    private void Update()
    {
        SelectEnemy();
    }

    private void SelectEnemy()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(camera.ScreenToWorldPoint(Input.mousePosition),
                Vector2.zero, Mathf.Infinity, LayerMask.GetMask("Enemy"));
            if (hit.collider != null)
            {
                SelectedEnemy = hit.collider.GetComponent<EnemyInteraction>();
                EnemyHealth enemyHealth = SelectedEnemy.GetComponent<EnemyHealth>();

                if(enemyHealth.Health > 0f)
                {
                    EventEnemySelected?.Invoke(SelectedEnemy);
                }
                else
                {
                    EnemyLoot loot = SelectedEnemy.GetComponent<EnemyLoot>();
                    LootManager.Instance.ShowLoot(loot);
                }
            }
            else
            {
                EventObjectNoSelected?.Invoke();
            }
        }
    }
}
