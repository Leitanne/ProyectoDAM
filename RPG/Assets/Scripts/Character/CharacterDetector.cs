using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDetector : MonoBehaviour
{
    public static Action<EnemyInteraction> EventEnemyDetected;
    public static Action EventEnemyLost;

    public EnemyInteraction EnemyDetected { get; private set; }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyDetected = other.GetComponent<EnemyInteraction>();
            
            if (EnemyDetected.GetComponent<EnemyHealth>().Health > 0)
            {
                EventEnemyDetected?.Invoke(EnemyDetected);
            }

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            EventEnemyLost?.Invoke();
        }
    }
}
