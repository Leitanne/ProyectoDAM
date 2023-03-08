using System;
using UnityEngine;

public enum attributeType
{
    strength,
    intelligence,
    dextrity
}

public class AttributeButton : MonoBehaviour
{
    public static Action<attributeType> addPointEvent;
    [SerializeField] private attributeType type;

    public void AddPoints()
    {
        addPointEvent?.Invoke(type);
    }
}
