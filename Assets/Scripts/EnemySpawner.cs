using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float spawnRate = 3f;
    
    private float cooldown = 0f;

    ObjectPooler objectPooler;


    void Start()
    {
        objectPooler = ObjectPooler.Instance;

    }


    void Update()
    {
    cooldown -= Time.deltaTime;
        if(cooldown <= 0){
            spawnEnemy();
            cooldown = spawnRate;
        }
    }

    
    void spawnEnemy(){
        GameObject enemy = objectPooler.SpawnFromPool(ObjectPooler.PoolType.Enemy,transform.position,Quaternion.identity, this.transform.parent);
    }
}
