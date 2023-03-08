using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMana : MonoBehaviour
{
    [SerializeField] private float initialMana;
    [SerializeField] private float maxMana;
    [SerializeField] private float regenerationRate; //Por segundo

    public float CurrentMana { get; private set; }
    public bool canRestoreMana => CurrentMana < maxMana;

    private CharacterHealth characterHealth;

    private void Awake()
    {
        characterHealth = GetComponent<CharacterHealth>();
    }

    //Inicializa el valor al inicio
    void Start()
    {
        CurrentMana = initialMana;
        UpdateManaBar();

        //Le indicamos que se repita una vez cada segundo, ya que el rate de generacion es por segundo
        InvokeRepeating(nameof(RegenerateMana), 1, 1);
    }

    //Se establece una tecla para poder testear la funcionalidad
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            UseMana(10f);
        }
    }

    //Realiza la operacion de restar el mana si hay suficiente mana.
    public void UseMana(float amount)
    {
        if(CurrentMana >= amount)
        {
            CurrentMana -= amount;
            UpdateManaBar();
        }
    }

    public void RestoreMana(float amount)
    {
        if(CurrentMana >= maxMana)
        {
            return;
        }

        CurrentMana += amount;
        if(CurrentMana > maxMana)
        {
            CurrentMana = maxMana;
        }

        UIManager.Instance.UpdateCharacterMana(CurrentMana, maxMana);
    }

    private void RegenerateMana()
    {
        if(characterHealth.Health > 0f && CurrentMana < maxMana)
        {
            CurrentMana += regenerationRate;
            UpdateManaBar();
        }
    }

    //Metodo que restablece todo el Mana
    public void RestoreFullMana()
    {
        CurrentMana = initialMana;
        UpdateManaBar();
    }

    //Accede al singleton de UIManager y llama al metodo de actualizar
    private void UpdateManaBar()
    {
        UIManager.Instance.UpdateCharacterMana(CurrentMana, maxMana);
    }
}
