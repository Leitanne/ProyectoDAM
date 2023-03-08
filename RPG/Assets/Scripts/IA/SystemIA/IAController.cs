using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public enum AttackTypes
{
    Melee,
    Assault
}

public class IAController : MonoBehaviour
{
    public static Action<float> EventDamageDone;

    [Header("Stats")]
    [SerializeField] private CharacterStats stats;

    [Header("Estados")]
    [SerializeField] private IAState initialState;
    [SerializeField] private IAState defaultState;

    [Header("Config")]
    [SerializeField] private float detectionRange;
    [SerializeField] private float attackRange;
    [SerializeField] private float assaultRange;
    [SerializeField] private float speedMovement;
    [SerializeField] private float assaultSpeed;
    [SerializeField] private LayerMask characterLayerMask;

    [Header("Ataque")]
    [SerializeField] private float damage;
    [SerializeField] private float attackCooldown;
    [SerializeField] private AttackTypes attackType;

    [Header("Debug")]
    [SerializeField] private bool showDetection;
    [SerializeField] private bool showAttackRange;
    [SerializeField] private bool showAssaultRange;

    private float nextAttackTimer;
    private BoxCollider2D _boxCollider2D;

    public Transform CharacterReference { get; set; }
    public IAState CurrentState { get; set; }
    public EnemyMovement EnemyMovement { get; set; }
    public float DetectionRange => detectionRange;
    public float Damage => damage;
    public AttackTypes AttackType => attackType;
    public float SpeedMovement => speedMovement;
    public LayerMask CharacterLayerMask => characterLayerMask;
    public float AttackRange => attackType == AttackTypes.Assault ? assaultRange : attackRange;


    private void Start()
    {
        _boxCollider2D = GetComponent<BoxCollider2D>();
        CurrentState = initialState;
        EnemyMovement = GetComponent<EnemyMovement>();
    }

    private void Update()
    {
        CurrentState.ExecuteState(this);
    }

    public void ChangeState(IAState newState)
    {
        if(newState != defaultState)
        {
            CurrentState = newState;
        }
    }

    public void MeleeAttack(float amount)
    {
        if (CharacterReference != null)
        {
            ApplyDamageToCharacter(amount);
        }
    }

    public void AssaultAttack(float amount)
    {
        StartCoroutine(IEAssault(amount));
    }

    private IEnumerator IEAssault (float amount)
    {
        Vector3 characterPosition = CharacterReference.position;
        Vector3 initialPosition = transform.position;
        Vector3 directionToCharacter = (characterPosition - initialPosition).normalized;
        Vector3 attackPosition = characterPosition - directionToCharacter * 0.5f;
        _boxCollider2D.enabled = false;

        float attackTransition = 0f;

        while(attackTransition <= 1f)
        {
            attackTransition += Time.deltaTime * speedMovement;
            float interpolation = (-Mathf.Pow(attackTransition, 2) + attackTransition) * 4f;
            transform.position = Vector3.Lerp(initialPosition, attackPosition, interpolation);
            yield return null;
        }

        if(CharacterReference != null)
        {
            ApplyDamageToCharacter(amount);
        }

        _boxCollider2D.enabled = true;
    }

    public void ApplyDamageToCharacter(float amount)
    {
        float damageToDo = 0;

        if(Random.value < stats.blockChance / 100)
        {
            return;
        }

        damageToDo = Mathf.Max(amount - stats.defense, 1f);
        CharacterReference.GetComponent<CharacterHealth>().TakeDamage(damageToDo);
        EventDamageDone?.Invoke(damageToDo);
    }

    public bool CharacterInAttackRange(float range)
    {
        float distanceToCharacter = (CharacterReference.position - transform.position).sqrMagnitude;
        if (distanceToCharacter < Mathf.Pow(range, 2))
        {
            return true;
        }

        return false;
    }

    public bool CanAttackAgain()
    {
        if(Time.time > nextAttackTimer)
        {
            return true;
        }

        return false;
    }

    public void UpdateTimeBetweenAttacks()
    {
        nextAttackTimer = Time.time + attackCooldown;
    }

    private void OnDrawGizmos()
    {
        if(showDetection)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, detectionRange);
        }

        if(showAttackRange)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackRange);
        }

        if (showAssaultRange)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, assaultRange);
        }
    }

}
