using System;
using UnityEngine;


public class EnemyHealth : HealthBasics
{
    public static Action<float> EventEnemyDefeated;

    [SerializeField] private EnemyHealthBar healthBarPrefab;
    [SerializeField] private Transform healthBarPosition;

    [Header("Remains")]
    [SerializeField] private GameObject remains;

    private EnemyHealthBar _enemyHealthBarCreated;
    private EnemyInteraction _enemyInteraction;
    private EnemyMovement _enemyMovement;
    private EnemyLoot _enemyLoot;
    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _boxCollider2D;
    private IAController _controller;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _controller = GetComponent<IAController>();
        _enemyLoot = GetComponent<EnemyLoot>();
        _enemyInteraction = GetComponent<EnemyInteraction>();
        _enemyMovement = GetComponent<EnemyMovement>();
    }

    protected override void Start()
    {
        base.Start();
        CreateHealthBar();
    }
    private void CreateHealthBar()
    {
        _enemyHealthBarCreated = Instantiate(healthBarPrefab, healthBarPosition);
        UpdateHealthBar(Health, maxHealth);
    }

    protected override void UpdateHealthBar(float currentHealth, float maximumHealth)
    {
        _enemyHealthBarCreated.ModifyHealth(currentHealth, maximumHealth);
    }

    protected override void Defeat()
    {
        DisableEnemy();
        EventEnemyDefeated?.Invoke(_enemyLoot.ExpWon);
        QuestManager.Instance.AddProgress("Kill10", 1);
        QuestManager.Instance.AddProgress("Kill25", 1);
        QuestManager.Instance.AddProgress("Kill50", 1);
    }

    private void DisableEnemy()
    {
        remains.SetActive(true);
        _enemyHealthBarCreated.gameObject.SetActive(false);
        _spriteRenderer.enabled = false;
        _controller.enabled = false;
        _boxCollider2D.isTrigger = false;
        _enemyInteraction.DisableSpritesSelection();
        _enemyMovement.enabled = false;
        
    }
}
