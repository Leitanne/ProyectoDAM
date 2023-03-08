using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DetectionType
{
    Range,
    Melee
}

public class EnemyInteraction : MonoBehaviour
{
    [SerializeField] private GameObject selectionRangeFX;
    [SerializeField] private GameObject selectionMeleeFX;

    public void ShowSelectedEnemy(bool state, DetectionType type)
    {
        if (type == DetectionType.Range)
        {
            selectionRangeFX.SetActive(state);
        }
        else
        {
            selectionMeleeFX.SetActive(state);
        }
    }

    public void DisableSpritesSelection()
    {
        selectionMeleeFX.SetActive(false);
        selectionRangeFX.SetActive(false);
    }
}
