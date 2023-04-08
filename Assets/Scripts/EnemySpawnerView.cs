using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemySpawnerView : MonoBehaviour
{
    [SerializeField] private float spawnRate = 3f;
    [SerializeField] private EnemyConfig _enemyConfig;
    public int GetEnemiesAmount{
        get { return _enemyConfig.enemiesAmount;}
    }
    LevelManagerController _levelManagerController;
    ObjectPooler _objectPooler;
    private float cooldown = 1f;    
    private Action _spawn;
    private Action _levelWon;
    bool _lastEnemy;
    public Transform MyTransform{
        get { return this.gameObject.transform;}
    }

    void Update()
    {
    cooldown -= Time.deltaTime;
        if(cooldown <= 0 && !_lastEnemy){
            _spawn(); 
            cooldown = spawnRate;
        }
    }
    
    public void CheckCooldown(Action spawn){
        _spawn = spawn;
    }

    public void LastEnemySent(){
        _lastEnemy = true;
    }

    
}




