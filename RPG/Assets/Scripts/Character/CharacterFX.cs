using System;
using System.Collections;
using UnityEngine;

enum CharType
{
    Player,
    IA
}

public class CharacterFX : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private GameObject canvasAnimationTextPrefab;
    [SerializeField] private Transform canvasTextPosition;

    [Header("Pooler")]
    [SerializeField] private ObjectPooler pooler;

    [Header("Type")]
    [SerializeField] private CharType charType;

    private EnemyHealth _enemyHealth;

    private void Awake()
    {
        _enemyHealth = GetComponent<EnemyHealth>();
    }
    private void Start()
    {
        pooler.CreatePooler(canvasAnimationTextPrefab);
    }

    private IEnumerator IEShowText(float amount, Color color)
    {
        GameObject newTextGO = pooler.GetInstance();
        AnimationText texto = newTextGO.GetComponent<AnimationText>();
        texto.DamageTextWrite(amount, color);
        newTextGO.transform.SetParent(canvasTextPosition);
        newTextGO.transform.position = canvasTextPosition.position;
        newTextGO.SetActive(true);

        yield return new WaitForSeconds(1f);
        newTextGO.SetActive(false);
        newTextGO.transform.SetParent(pooler.ContainerList.transform);
    }

    private void ResponseDamageToCharacter(float damage)
    {
        StartCoroutine(IEShowText(damage, Color.red));
    }

    private void ResponseDamageToEnemy(float damage, EnemyHealth enemyHealth)
    {
        if (charType == CharType.IA && _enemyHealth == enemyHealth)
        {
            StartCoroutine(IEShowText(damage, Color.black));
        }
    }

    private void OnEnable()
    {
        IAController.EventDamageDone += ResponseDamageToCharacter;
        CharacterAttack.EventDamagedEnemy += ResponseDamageToEnemy;
    }

    private void OnDisable()
    {
        IAController.EventDamageDone -= ResponseDamageToCharacter;
        CharacterAttack.EventDamagedEnemy -= ResponseDamageToEnemy;
    }
}
