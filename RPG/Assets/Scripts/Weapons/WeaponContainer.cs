using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class WeaponContainer : Singleton<WeaponContainer>
{
    [SerializeField] private Image weaponIcon;
    [SerializeField] private Image weaponSkillIcon;

    public WeaponItem EquipedWeapon { get; set; }


    public void EquipWeapon(WeaponItem weaponItem)
    {
        EquipedWeapon = weaponItem;
        weaponIcon.sprite = weaponItem.Weapon.WeaponIcon;
        weaponIcon.gameObject.SetActive(true);

        if (weaponItem.Weapon.Type == WeaponType.Magic)
        {
            weaponSkillIcon.sprite = weaponItem.Weapon.SkillIcon;
            weaponSkillIcon.gameObject.SetActive(true);
        }
        
        Inventory.Instance.Character.characterAttack.EquipWeapon(weaponItem);
    }
    
    public void RemoveWeapon()
    {
        weaponIcon.gameObject.SetActive(false);
        weaponSkillIcon.gameObject.SetActive(false);
        EquipedWeapon = null;
        Inventory.Instance.Character.characterAttack.RemoveWeapon();
    }
}
