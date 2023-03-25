using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemySpawnerView : MonoBehaviour
{
    [SerializeField] private float spawnRate = 3f;
    [SerializeField] private int enemiesCount;
    public int GetEnemiesCount{
        get { return enemiesCount;}
    }
    LevelManagerController _levelManagerController;
    ObjectPooler _objectPooler;
    private float cooldown = 1f;    
    private Action _spawn;
    private Action _levelWon;
    bool _lastEnemy;

    void Update()
    {
    cooldown -= Time.deltaTime;
        if(cooldown <= 0 && !_lastEnemy){
            _spawn(); 
            cooldown = spawnRate;
        }
        else if(_lastEnemy){
            StartCoroutine(LastEnemySent());
        }
    }
    
    public void CheckCooldown(Action spawn){
        _spawn = spawn;
    }

    public void WaitLevelWon(Action levelWon){
        _lastEnemy = true;
        _levelWon = levelWon;
    }

    private IEnumerator LastEnemySent(){
        yield return new WaitForSeconds( 15.0f );
        _levelWon();
    }


    
}




