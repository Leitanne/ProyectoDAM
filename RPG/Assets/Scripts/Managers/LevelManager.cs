using System;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Character character;
    [SerializeField] private Transform respawnPoint;

    //Cuando se pulsa la tecla R, el personaje resucita en el respawn point indicado.
    private void Update()
    {
        //Respawn event
        if(Input.GetKeyDown(KeyCode.R))
        {
            if (character.characterHealth.Defeated)
            {
                character.transform.localPosition = respawnPoint.position;
                character.Revive();
            }
        }
        
    }
}
