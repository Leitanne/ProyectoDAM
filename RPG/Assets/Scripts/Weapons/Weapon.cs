using UnityEngine;

public enum WeaponType
{
    Magic,
    Melee
}

[CreateAssetMenu(menuName = "Character/Weapon")]
public class Weapon : ScriptableObject
{
    [Header("Config")] 
    public Sprite WeaponIcon;
    public Sprite SkillIcon;
    public WeaponType Type;
    public float Damage;

    [Header("Magic Weapon")] 
    public Projectile ProjectilePrefab;
    public float RequiredMana;

    [Header("Stats")] 
    public float CritChance;
    public float BlockChance;
}
