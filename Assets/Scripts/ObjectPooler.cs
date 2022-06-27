using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [System.Serializable]
    public class Pool{
        public PoolType type;
        public GameObject prefab;
        public int size;
    }

    public enum PoolType
    {
        Tower=0,
        Enemy=10,
        Projectile=20
    }

    #region Singleton
    public static ObjectPooler Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }

    #endregion

    public List<Pool> pools;
    public Dictionary<PoolType, Queue<GameObject>> poolDictionary;

    void Start()
    {
        poolDictionary = new Dictionary<PoolType, Queue<GameObject>>();    

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.type, objectPool);
        }
    }

    public GameObject SpawnFromPool(PoolType type, Vector3 position, Quaternion rotation, Transform parent)
    {
        if (!poolDictionary.ContainsKey(type))
        {
            Debug.LogWarning("Pool with tag " + type + " doesn't exist.");
            return null;
        }
        
        GameObject objectToSpawn = poolDictionary[type].Dequeue();

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;
        objectToSpawn.transform.SetParent(parent);
        IPooledObject pooledObj = objectToSpawn.GetComponent<IPooledObject>();
            
        
        switch (type)
        {
        case PoolType.Projectile:     

            if (pooledObj != null)
            {
                pooledObj.OnObjectSpawn();
            }
            break;
        case PoolType.Tower:
            
            break;
        case PoolType.Enemy:
            if (pooledObj != null)
            {
                pooledObj.OnObjectSpawn();
            }
            break;
        }

        poolDictionary[type].Enqueue(objectToSpawn);

        return objectToSpawn;
    }

    
    void Update()
    {
        
    }
}

