using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemySpawnerView : MonoBehaviour, IPooledObject
{
    [SerializeField] private EnemySpawnerConfig _enemySapwnerConfig;
    public int GetEnemiesAmount{
        get { return _enemySapwnerConfig._enemiesAmount;}
    }
    private float _spawnRate;
    LevelManagerController _levelManagerController;
    ObjectPooler _objectPooler;
    private float cooldown = 1f;    
    private Action _spawn;
    private Action _levelWon;
    bool _lastEnemy;
    public Transform MyTransform{
        get { return this.gameObject.transform;}
    }

    public void OnObjectSpawn(){   
            _spawnRate = _enemySapwnerConfig._enemiesSpawnRate;
    }

    void Update()
    {
    cooldown -= Time.deltaTime;
        if(cooldown <= 0 && !_lastEnemy){
            _spawn(); 
            cooldown = _spawnRate;
        }
    }
    
    public void CheckCooldown(Action spawn){
        _spawn = spawn;
    }

    public void LastEnemySent(){
        _lastEnemy = true;
    }

}




