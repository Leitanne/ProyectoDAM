using UnityEngine;
using System;

//Hereda de HealthBasics
public class CharacterHealth : HealthBasics
{
    //almacena una accion a ejecutar
    public static Action DefeatedCharacterEvent;

    public bool Defeated { get; private set; }
    //comprueba que el personje no esta a vida máxima antes de curarse
    public bool canBeHealed => Health < maxHealth;

    private BoxCollider2D boxCollider;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    protected override void Start()
    {
        base.Start();
        UpdateHealthBar(Health, maxHealth);
    }

    //Funcionalidades para probar los metodos de TakeDamage y RestoreHealth
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            TakeDamage(10);
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            restoreHealth(10);
        }
    }

    //Si el personaje puede ser curado, se le cura la cantidad introducida. Si el resultado sobrepasa la cantidad de vida 
    //maxima, la vida se iguala a la vida maxima.
    public void restoreHealth(float amount)
    {
        if(canBeHealed && !Defeated)
        {
            Health += amount;

            if(Health > maxHealth)
            {
                Health = maxHealth;
            }

            UpdateHealthBar(Health, maxHealth);
        }
    }

    //Comprueba que existe una suscripcion y, si existe, invoca el evento.
    protected override void Defeat()
    {
        if(DefeatedCharacterEvent != null)
        {
            boxCollider.enabled = false; //desactivamos el boxcollider para evitar colisiones con enemigos
            Defeated = true;
            DefeatedCharacterEvent.Invoke();
        }
    }

    //Accion de revivir en cuanto a barras de vida. Habilita el boxCollider otra vez, pone la bool en falso y restaura el valor y la barra
    //de vida.
    public void Revive()
    {
        boxCollider.enabled = true;
        Defeated = false;
        Health = initialHealth;
        UpdateHealthBar(Health, initialHealth);
    }

    protected override void UpdateHealthBar(float currentHealth, float maximumHealth)
    {
        UIManager.Instance.UpdateCharacterHealth(currentHealth, maximumHealth);
    }
}
