using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] private Image healthBar;

    private float currentHealth;
    private float maxHealth;

    private void Update()
    {
        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, 
            currentHealth / maxHealth, 10f * Time.deltaTime);
    }

    public void ModifyHealth(float pCurrentHealth, float pMaxHealth)
    {
        currentHealth = pCurrentHealth;
        maxHealth = pMaxHealth;
    }
}
