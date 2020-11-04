using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PoolObj
{
    public string poolName;
    public GameObject poolPrefab;
    public int size;
}

public class PoolManager : SingletonManager<PoolManager>
{
    public List<PoolObj> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;


    private void Awake()
    {
       // DontDestroyOnLoad(this.gameObject);

    }
    private void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (PoolObj pool in pools)
        {
            Queue<GameObject> objectToPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject gObj = Instantiate(pool.poolPrefab);
                gObj.SetActive(false);
                gObj.transform.parent = this.transform;
                objectToPool.Enqueue(gObj);

            }

            poolDictionary.Add(pool.poolName, objectToPool);
        }

    }

    public GameObject SpawnInWorld(string tag,Vector3 position,Quaternion rotation)
    {
        if(!poolDictionary.ContainsKey(tag))
        {
            Debug.LogError("Object with tag " + tag + " not found...");
            return null;
        }

        GameObject objectToSpawn = poolDictionary[tag].Dequeue();

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;


        poolDictionary[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }
}
