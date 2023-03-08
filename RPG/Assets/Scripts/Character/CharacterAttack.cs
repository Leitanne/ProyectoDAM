using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class CharacterAttack : MonoBehaviour
{
    public static Action<float, EnemyHealth> EventDamagedEnemy;
    
    [Header("Stats")] 
    [SerializeField] private CharacterStats stats;

    [Header("Pooler")] 
    [SerializeField] private ObjectPooler pooler;

    [Header("Attack")] 
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform[] shootingPositions;
    
    public Weapon EquipedWeapon { get; private set; }
    public EnemyInteraction EnemyObjective { get; private set; }
    public bool Attacking { get; set; }
    
    private CharacterMana _characterMana;
    private int indexShootingDirection;
    private float timeTillNextAttack;

    private void Awake()
    {
        _characterMana = GetComponent<CharacterMana>();
    }

    private void Update()
    {
        GetShootingDirection();

        if (Time.time > timeTillNextAttack)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (EquipedWeapon == null || EnemyObjective == null)
                {
                    return;
                }
                
                UseWeapon();
                timeTillNextAttack = Time.time + attackCooldown;
                StartCoroutine(IEAttackCondition());
            }
        }

    }
    
    private void UseWeapon()
    {
        if (EquipedWeapon.Type == WeaponType.Magic)
        {
            if (_characterMana.CurrentMana < EquipedWeapon.RequiredMana)
            {
                return;
            }

            GameObject newProjectile = pooler.GetInstance();
            newProjectile.transform.localPosition = shootingPositions[indexShootingDirection].position;

            Projectile projectile = newProjectile.GetComponent<Projectile>();
            projectile.InitializeProjectile(this);
            
            newProjectile.SetActive(true);
            _characterMana.UseMana(EquipedWeapon.RequiredMana);
        }
        else
        {
            float damage = GetDamage();
            EnemyHealth enemyHealth = EnemyObjective.GetComponent<EnemyHealth>();
            enemyHealth.TakeDamage(damage);
            EventDamagedEnemy?.Invoke(damage, enemyHealth);
        } 
    }
    
    public float GetDamage()
    {
        float amount = stats.attack;
        if (Random.value < stats.critChance / 100)
        {
            amount *= 2;
        }

        return amount;
    }   
    
    private IEnumerator IEAttackCondition()
    {
        Attacking = true;
        yield return new WaitForSeconds(0.3f);
        Attacking = false;
    }
    
    public void EquipWeapon(WeaponItem weaponToEquip)
    {
        EquipedWeapon = weaponToEquip.Weapon;
        if (EquipedWeapon.Type == WeaponType.Magic)
        {
            pooler.CreatePooler(EquipedWeapon.ProjectilePrefab.gameObject);
        }
        
        stats.AddWeaponBonus(EquipedWeapon);
    }

    public void RemoveWeapon()
    {
        if (EquipedWeapon == null)
        {
            return;
        }

        if(EquipedWeapon.Type == WeaponType.Magic)
        {
            pooler.DestroyPooler();
        }
        
        stats.RemoveWeaponBonus(EquipedWeapon);
        EquipedWeapon = null;
        
    }
    private void GetShootingDirection()
    {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (input.x > 0.1f)
        {
            indexShootingDirection = 1;
        }
        else if (input.x < 0f)
        {
            indexShootingDirection = 3;
        }
        else if (input.y > 0.1f)
        {
            indexShootingDirection = 0;
        }
        else if (input.y < 0f)
        {
            indexShootingDirection = 2;
        }
    }
    
    private void EnemyRangeSelected(EnemyInteraction selectedEnemy)
    {
        if (EquipedWeapon == null) { return; }
        if (EquipedWeapon.Type != WeaponType.Magic) { return; }
        if (EnemyObjective == selectedEnemy) { return; }

        EnemyObjective = selectedEnemy;
        EnemyObjective.ShowSelectedEnemy(true, DetectionType.Range);
    }
    
    private void EnemyNoSelected()
    {
        if (EnemyObjective == null) { return; }
        EnemyObjective.ShowSelectedEnemy(false, DetectionType.Range);
        EnemyObjective = null;
    }
 
    private void MeleeEnemyDetected(EnemyInteraction enemyDetected)
    {
        if (EquipedWeapon == null) { return; }
        if (EquipedWeapon.Type != WeaponType.Melee) { return; }
        EnemyObjective = enemyDetected;
        EnemyObjective.ShowSelectedEnemy(true, DetectionType.Melee);
    }

    private void MeleeEnemyLost()
    {
        if (EquipedWeapon == null) { return; }
        if (EnemyObjective == null) { return; }
        if (EquipedWeapon.Type != WeaponType.Melee) { return; }
        EnemyObjective.ShowSelectedEnemy(false, DetectionType.Melee);
        EnemyObjective = null;
    }


    private void OnEnable()
    {
        SelectionManager.EventEnemySelected += EnemyRangeSelected;
        SelectionManager.EventObjectNoSelected += EnemyNoSelected;
        CharacterDetector.EventEnemyDetected += MeleeEnemyDetected;
        CharacterDetector.EventEnemyLost += MeleeEnemyLost;
    }

    private void OnDisable()
    {
        SelectionManager.EventEnemySelected -= EnemyRangeSelected;
        SelectionManager.EventObjectNoSelected -= EnemyNoSelected;
        CharacterDetector.EventEnemyDetected -= MeleeEnemyDetected;
        CharacterDetector.EventEnemyLost -= MeleeEnemyLost;
    }
    
}
