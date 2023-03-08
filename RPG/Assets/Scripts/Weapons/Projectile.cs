using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    
    [Header("Config")]
    [SerializeField] private float speed;

    public CharacterAttack CharacterAttack { get; private set; }
    
    private Rigidbody2D _rigidbody2D;
    private Vector2 direction;
    private EnemyInteraction enemyObjective;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (enemyObjective == null)
        {
            return;
        }
        
        MoveProjectile();
    }

    private void MoveProjectile()
    {
        direction = enemyObjective.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        _rigidbody2D.MovePosition(_rigidbody2D.position + direction.normalized * speed * Time.fixedDeltaTime);
    }

    public void InitializeProjectile(CharacterAttack atack)
    {
        CharacterAttack = atack;
        enemyObjective = atack.EnemyObjective;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            float damage = CharacterAttack.GetDamage();
            EnemyHealth enemyHealth = enemyObjective.GetComponent<EnemyHealth>();
            enemyHealth.TakeDamage(damage);
            CharacterAttack.EventDamagedEnemy?.Invoke(damage, enemyHealth);
            gameObject.SetActive(false);
        }
    }
  
}