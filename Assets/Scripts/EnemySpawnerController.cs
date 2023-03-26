using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Zenject;
using UniRx;

public class EnemySpawnerController : IPooledObject, IDisposable
{
    LevelManagerController _levelManagerController;
    ObjectPooler _objectPooler;
    EnemySpawnerView _enemySpawnerView;
    private int enemiesAmount;

    public EnemySpawnerController(EnemySpawnerView enemySpawnerView, ObjectPooler objectPooler, LevelManagerController levelManagerController) {
        _enemySpawnerView = enemySpawnerView;
        _objectPooler = objectPooler;
        _levelManagerController = levelManagerController;
        OnObjectSpawn();
    }


    public void OnObjectSpawn(){
        enemiesAmount = _enemySpawnerView.GetEnemiesCount;
        _levelManagerController.enemiesCount = enemiesAmount;
        _levelManagerController.DisplayEnemiesCount();
        _enemySpawnerView.CheckCooldown(() => {spawnEnemy(); });
    }

    void spawnEnemy(){
        _objectPooler.SpawnObject(ObjectPooler.PoolType.Enemy,_enemySpawnerView.transform.position,Quaternion.identity, _enemySpawnerView.transform);
        enemiesAmount -- ;
        if(enemiesAmount <= 0) _enemySpawnerView.LastEnemySent();
    }

    void LevelWon(){
        Dispose();
    }

    public void Dispose(){
    }

}
