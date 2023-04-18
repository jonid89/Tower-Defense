using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Zenject;
using UniRx;

public class EnemySpawnerController : IPooledObject, IDisposable
{
    ObjectPooler _objectPooler;
    EnemySpawnerView _enemySpawnerView;
    private int enemiesToSpawn;
    public int GetEnemiesAmount{
        get { return enemiesToSpawn;}
    }

    public EnemySpawnerController(EnemySpawnerView enemySpawnerView, ObjectPooler objectPooler) {
        _enemySpawnerView = enemySpawnerView;
        _objectPooler = objectPooler;
        OnObjectSpawn();
        _enemySpawnerView.OnObjectSpawn();
    }


    public void OnObjectSpawn(){
        enemiesToSpawn = _enemySpawnerView.GetEnemiesAmount;
        _enemySpawnerView.CheckCooldown(() => {spawnEnemy(); });
    }

    void spawnEnemy(){
        _objectPooler.SpawnObject(ObjectPooler.PoolType.Enemy,_enemySpawnerView.transform.position,Quaternion.identity, _enemySpawnerView.transform);
        enemiesToSpawn -- ;
        if(enemiesToSpawn <= 0) _enemySpawnerView.LastEnemySent();
    }

    void LevelWon(){
        Dispose();
    }

    public void Dispose(){
    }

}
