using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float spawnRate = 3f;
    [SerializeField] private int enemiesCount;
    LevelManagerController _levelManagerController;

    ObjectPooler _objectPooler;
    private float cooldown = 0f;    

    [Inject]
    public void Construct (ObjectPooler objectPooler, LevelManagerController levelManagerController) {
        _objectPooler = objectPooler;
        _levelManagerController = levelManagerController;
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
        yield return new WaitForSeconds( 15.0f );
        _levelManagerController.LevelWon();
    }
}




