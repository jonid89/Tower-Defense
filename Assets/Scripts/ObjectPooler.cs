using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler
{

    private EnemyView.Pool _enemyViewPool;
    private Tower.Factory _towerFactory;
    private Projectile.Factory _projectileFactory;
    private EnemyController.Factory _enemyControllerFactory;

    private HealthBar _healthBar;

    private EnemyPath _enemyPath;


    public enum PoolType
    {
        Tower=0,
        Enemy=10,
        Projectile=20
    }


    public ObjectPooler(EnemyView.Pool enemyViewPool, EnemyController.Factory enemyControllerFactory, HealthBar healthBar, EnemyPath enemyPath, Tower.Factory towerFactory, Projectile.Factory projectileFactory) 
    {
        _enemyViewPool = enemyViewPool;
        _enemyControllerFactory = enemyControllerFactory;
        _healthBar = healthBar;
        _enemyPath = enemyPath;
        _towerFactory = towerFactory;
        _projectileFactory =  projectileFactory;
    }



    public void SpawnObject(PoolType type, Vector3 position, Quaternion rotation, Transform parent){
        
        GameObject obj = new GameObject();
        
        switch (type)
        {
        case PoolType.Projectile: 
            Projectile _projectile = _projectileFactory.Create();
            obj = _projectile.gameObject;
            break;
        case PoolType.Enemy:
            var _enemyView = _enemyViewPool.Spawn();
            _enemyControllerFactory.Create(_enemyView, _healthBar, _enemyPath);
            //_enemyController.OnObjectSpawn();
            obj = _enemyView.gameObject;
            break;
        case PoolType.Tower:
            Tower _tower = _towerFactory.Create();
            obj = _tower.gameObject;
            break;
        }
        
        obj.SetActive(true);
        obj.transform.position = position;
        obj.transform.rotation = rotation;
        obj.transform.SetParent(parent);

    }





}
