using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Hace que en el menu de crear assets aparezca este script
[CreateAssetMenu(menuName = "Estadísticas")]
public class CharacterStats : ScriptableObject
{
    public float attack     = 5f;
    public float defense    = 2f;
    public float speed      = 5f;
    public float level;
    public float currentExp;
    public float requiredExp;
    [Range(0f, 100f)] public float critChance;
    [Range(0f, 100f)] public float blockChance;

    public int strength;
    public int intelligence;
    public int dextrity;
    [HideInInspector]public int avaliablePoints;

    public void IncreasePerStrengthPoint()
    {
        attack += 2f;
        defense += 1f;
        blockChance += 0.03f;
    }

    public void IncreasePerIntelligencePoint()
    {
        attack += 3f;
        critChance += 0.2f;
    }

    public void IncreasePerDextrityPoint()
    {
        speed += 0.1f;
        blockChance += 0.05f;
    }

    public void AddWeaponBonus(Weapon weapon)
    {
        attack += weapon.Damage;
        critChance += weapon.CritChance;
        blockChance += weapon.BlockChance;
    }

    public void RemoveWeaponBonus(Weapon weapon)
    {
        attack -= weapon.Damage;
        critChance -= weapon.CritChance;
        blockChance -= weapon.BlockChance;
    }

    public void ResetValues()
    {
        attack = 5f;
        defense = 2f;
        speed = 5f;
        level = 1;
        currentExp = 0f;
        requiredExp = 0f;
        critChance = 0f;
        blockChance = 0f;

        strength = 0;
        intelligence = 0;
        dextrity = 0;

        avaliablePoints = 0;
    }
}
