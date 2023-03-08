using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [SerializeField] private int amountToCreate;

    private List<GameObject> list;
    public GameObject ContainerList { get; private set; }

    public void CreatePooler(GameObject objectToCreate)
    {
        list = new List<GameObject>();
        ContainerList = new GameObject($"Pool - {objectToCreate.name}");

        for (int i = 0; i < amountToCreate; i++)
        {
            list.Add(AddInstance(objectToCreate));
        }
    }
    
    private GameObject AddInstance(GameObject objectToCreate)
    {
        GameObject newObject = Instantiate(objectToCreate, ContainerList.transform);
        newObject.SetActive(false);
        return newObject;
    }

    public GameObject GetInstance()
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].activeSelf == false)
            {
                return list[i];
            }
        }

        return null;
    }

    public void DestroyPooler()
    {
        Destroy(ContainerList);
        list.Clear();
    }
}
