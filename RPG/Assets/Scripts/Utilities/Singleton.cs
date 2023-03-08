using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Component
{
    private static T instance;

    //instancia el objeto
    public static T Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<T>();
                //si aun asi no es capaz de reconocer el objeto, creamos un gameobject y le añadimos el componente
                if(instance == null)
                {
                    GameObject gameObject = new GameObject();
                    instance = gameObject.AddComponent<T>();
                }
            }

            return instance;
        }
    }

    //Configura que esta instancia siempre será unica.
    protected virtual void Awake()
    {
        instance = this as T;
    }

}
