using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float spawnRate = 3f;
    [SerializeField] private int enemiesCount = 5;
    [SerializeField] LevelManager levelManager;
    
    ObjectPooler objectPooler;
    private float cooldown = 0f;    
    private List<GameObject> enemies = new List<GameObject>();

    void Start()
    {
        objectPooler = ObjectPooler.Instance;

    }


    void Update()
    {
    cooldown -= Time.deltaTime;
        if(cooldown <= 0 && enemiesCount > 0){
            spawnEnemy();
            cooldown = spawnRate;
            enemiesCount -= 1;
        }
        if(enemiesCount <= 0){
            StartCoroutine(LastEnemySent());
        }
    }

    
    void spawnEnemy(){
        GameObject enemy = objectPooler.SpawnFromPool(ObjectPooler.PoolType.Enemy,transform.position,Quaternion.identity, this.transform.parent);
        enemies.Add(enemy);
    }

    private IEnumerator LastEnemySent(){
        yield return new WaitForSeconds( 10.0f );
        levelManager.LevelWon();
    }
}
