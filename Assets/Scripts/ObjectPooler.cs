using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [System.Serializable]

    public class Pool
    {
        public string type;

        public GameObject prefab;

        public int size;
    }

    public static ObjectPooler instance;

    private void Awake()
    {
        instance = this;
    }

    public List<Pool> pools;

    public Dictionary<string, Queue<GameObject>> poolDictionary;

    GameObject objectToSpawn;

    private void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach(Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for(int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.type, objectPool);
        }
    }

    public GameObject SpawnFromPool(string type, Vector2 posistion, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(type))
        {
            Debug.LogWarning("Pool with type : " + type + " doesn't exist.");
            return null;
        }

        objectToSpawn = poolDictionary[type].Dequeue();
        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = posistion;
        objectToSpawn.transform.rotation = rotation;

        poolDictionary[type].Enqueue(objectToSpawn);

        return objectToSpawn;

    }
}
