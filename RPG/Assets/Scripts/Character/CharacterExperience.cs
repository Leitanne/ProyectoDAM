using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterExperience : MonoBehaviour
{
    [SerializeField] private CharacterStats stats;

    //Variables del inspector
    [SerializeField] private int maxLevel; //Nivel maximo
    [SerializeField] private int neededExp; //Experiencia necesaria por nivel
    [SerializeField] private int expIncrement; //Proporcion de incremento de experiencia necesaria por nivel

    private float totalExp;
    private float currentExp; //Experiencia actual
    private float expNextLevel; //Experiencia requerida para el siguiente nivel

    // Start is called before the first frame update
    void Start()
    {
        //Ponemos las variables a un nivel basico
        stats.level = 1;
        expNextLevel = neededExp;
        stats.requiredExp = expNextLevel;
        UpdateExperienceBar();
    }

    // Update is called once per frame
    void Update()
    {
        //Para probar la funcionalidad
        if(Input.GetKeyDown(KeyCode.X))
        {
            AddExperience(10f);
        }
    }

    //Recibe experiencia y se añade al personaje y verifica si puede subir de nivel
    public void AddExperience(float obtainedExp)
    {
        if(obtainedExp > 0f && stats.level < maxLevel) //Si mayor a 0
        {
            float expLeftTillNextLevel = expNextLevel - currentExp; //calculamos cuanta exp falta para el proximo nivel

            //Si la experiencia obtenida es mayor o igual a la que falta para el proximo nivel, sube de nivel,
            //de otra manera suma a la experiencia actual la experiencia obtenida.

            while(obtainedExp >= expLeftTillNextLevel)
            {
                obtainedExp -= expLeftTillNextLevel;
                LevelUp();
                expLeftTillNextLevel = expNextLevel - currentExp;
            }

            if(obtainedExp < expLeftTillNextLevel)
            {
                currentExp += obtainedExp;
            }
        }

        stats.currentExp = currentExp;

        UpdateExperienceBar();
    }

    //sube un nivel, pone la experiencia a 0 y calcula la exp necesaria para el proximo nivel
    private void LevelUp()
    {
        if(stats.level < maxLevel)
        {
            stats.level++;
            currentExp = 0f;
            expNextLevel *= expIncrement;
            stats.requiredExp = expNextLevel;
            stats.avaliablePoints += 3;
        }
    }

    //Metodo que actualiza la barra de experiencia
    private void UpdateExperienceBar()
    {
        UIManager.Instance.UpdateCharacterExperience(currentExp, expNextLevel);
    }

    private void ResponseEnemyDefeated(float exp)
    {
        AddExperience(exp);
    }

    private void OnEnable()
    {
        EnemyHealth.EventEnemyDefeated += ResponseEnemyDefeated;
    }

    private void OnDisable()
    {
        EnemyHealth.EventEnemyDefeated -= ResponseEnemyDefeated;
    }
}
