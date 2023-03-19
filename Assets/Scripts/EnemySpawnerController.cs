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
    private int enemiesCount;

    public EnemySpawnerController(EnemySpawnerView enemySpawnerView, ObjectPooler objectPooler, LevelManagerController levelManagerController) {
        _enemySpawnerView = enemySpawnerView;
        _objectPooler = objectPooler;
        _levelManagerController = levelManagerController;
        OnObjectSpawn();
    }


    public void OnObjectSpawn(){
        enemiesCount = _enemySpawnerView.GetEnemiesCount;
        _enemySpawnerView.CheckCooldown(() => {spawnEnemy(); });
    }

    void spawnEnemy(){
        _objectPooler.SpawnObject(ObjectPooler.PoolType.Enemy,_enemySpawnerView.transform.position,Quaternion.identity, _enemySpawnerView.transform);
        enemiesCount -- ;
        if(enemiesCount <= 0) _enemySpawnerView.WaitLevelWon(() => {LevelWon(); });
    }

    void LevelWon(){
        _levelManagerController.LevelWon();
        Dispose();
    }

    public void Dispose(){
    }

}
