using System;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private CharacterStats stats;

    public CharacterAttack      characterAttack { get; private set; }
    public CharacterExperience  characterExperience { get; private set; }
    public CharacterHealth      characterHealth { get; private set; }
    public PCAnimations         PCAnimations { get; private set; }
    public CharacterMana        characterMana { get; private set; } 

    private void Awake()
    {
        characterAttack     = GetComponent<CharacterAttack>();
        characterHealth     = GetComponent<CharacterHealth>();
        PCAnimations        = GetComponent<PCAnimations>();
        characterMana       = GetComponent<CharacterMana>();
        characterExperience = GetComponent<CharacterExperience>();
    }

    //Controla los eventos que deben suceder cuando un personaje resucita.
    public void Revive()
    {
        characterHealth.Revive();
        PCAnimations.ReviveAnimation();
        characterMana.RestoreFullMana();
    }

    private void AddAttributeResponse(attributeType type)
    {
        if(stats.avaliablePoints <= 0)
        {
            return;
        }

        switch (type)
        {
            case attributeType.strength:
                stats.IncreasePerStrengthPoint();
                stats.strength++;
                break;
            case attributeType.intelligence:
                stats.IncreasePerIntelligencePoint();
                stats.intelligence++;
                break;
            case attributeType.dextrity:
                stats.IncreasePerDextrityPoint();
                stats.dextrity++;
                break;
        }

        stats.avaliablePoints--;
    }

    private void OnEnable()
    {
        AttributeButton.addPointEvent += AddAttributeResponse;
    }

    private void OnDisable()
    {
        AttributeButton.addPointEvent -= AddAttributeResponse;
    }
}
