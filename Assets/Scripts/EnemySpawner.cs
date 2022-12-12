using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float spawnRate = 3f;
    [SerializeField] private int enemiesCount = 1;
    [SerializeField] LevelManager levelManager;

    ObjectPooler _objectPooler;
    private float cooldown = 0f;    
    private List<GameObject> enemies = new List<GameObject>();

    

    [Inject]
    public void Construct (ObjectPooler objectPooler) {
        _objectPooler = objectPooler;
    }



    void Update()
    {
    cooldown -= Time.deltaTime;
        if(cooldown <= 0 && enemiesCount > 0){
            spawnEnemy();
            cooldown = spawnRate;
            enemiesCount -= 1;
        }
        else if(enemiesCount <= 0){
            StartCoroutine(LastEnemySent());
        }
    }

    
    void spawnEnemy(){
        _objectPooler.SpawnObject(ObjectPooler.PoolType.Enemy,transform.position,Quaternion.identity, this.transform);
    }

    private IEnumerator LastEnemySent(){
        yield return new WaitForSeconds( 10.0f );
        levelManager.LevelWon();
    }
}
