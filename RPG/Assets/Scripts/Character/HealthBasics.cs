using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBasics : MonoBehaviour
{   
    //Deben ser visibles para el inspector. Contiene la vida inicial y la vida máxima.
    [SerializeField] protected float initialHealth;
    [SerializeField] protected float maxHealth;

    public float Health { get; protected set; }

    protected virtual void Start()
    {
        Health = initialHealth;
    }

    public void TakeDamage(float amount)
    {
        //Si el daño recibido es menor o igual a 0, se regresa. Esta puesto como protección.
        if(amount <= 0)
        {
            return;
        }

        //Si la vida es mayor a 0, descuenta el daño recibido y actualiza la barra de vida. Si una vez actualizada la vida es menor a 0, llama a derrota
        //pero si la vida es mayor a 0, se actualiza la vida.
        if (Health > 0f)
        {
            Health -= amount;
            if(Health <= 0f)
            {
                Health = 0f;
                UpdateHealthBar(Health, maxHealth);
                Defeat();
                return;
            }
            UpdateHealthBar(Health, maxHealth);
        }
    }
    protected virtual void UpdateHealthBar(float currentHealth, float maximumHealth)
    {

    }
    
    protected virtual void Defeat()
    {

    }
}
