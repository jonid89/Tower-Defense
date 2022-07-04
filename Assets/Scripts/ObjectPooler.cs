using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ObjectPooler : MonoBehaviour
{
    Enemy.Factory _enemyFactory;
    Tower.Factory _towerFactory;
    Projectile.Factory _projectileFactory;

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

    /*#region Singleton
    public static ObjectPooler Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }

    #endregion*/
    
    [Inject]
    public void Construct (Enemy.Factory enemyFactory, Tower.Factory towerFactory, Projectile.Factory projectileFactory) 
    {
        Debug.Log("enemyFactory");
        Debug.Log(enemyFactory);

        _enemyFactory = enemyFactory;
        _towerFactory = towerFactory;
        _projectileFactory =  projectileFactory;
    }

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
                GameObject obj; // = Instantiate(pool.prefab)

                switch (pool.type)
                {
                    case PoolType.Enemy:
                        obj = _enemyFactory.Create();
                        break;
                }

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
        if (poolDictionary[type].Count == 0) {
            Debug.LogWarning("Pool with tag " + type + " is empty");
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
        case PoolType.Enemy:
            if (pooledObj != null)
            {
                pooledObj.OnObjectSpawn();
            }
            break;
        case PoolType.Tower:
            
            break;
        }

        poolDictionary[type].Enqueue(objectToSpawn);

        return objectToSpawn;
    }


}

