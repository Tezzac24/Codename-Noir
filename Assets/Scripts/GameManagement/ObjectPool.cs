using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance;
    public GameObject bullet;
    [SerializeField] List<GameObject> pooledObjects = new List<GameObject>();
    [SerializeField] int countToPool;
    [SerializeField] bool canExpand;


    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

        for (int i = 0; i < countToPool; i++)
        {
            GameObject obj = Instantiate(bullet);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        if (canExpand)
        {
            GameObject obj = Instantiate(bullet);
            obj.SetActive(false);
            pooledObjects.Add(obj);
            return obj;
        }
        return null;
    }
}
