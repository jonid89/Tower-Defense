using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler
{

    private EnemyView.Pool _enemyViewPool;
    private ProjectileView.Pool _projectileViewPool;
    private Tower.Factory _towerFactory;
    private ProjectileController.Factory _projectileControllerFactory;
    private EnemyController.Factory _enemyControllerFactory;

    private HealthBar _healthBar;

    private EnemyPath _enemyPath;


    public enum PoolType
    {
        Tower=0,
        Enemy=10,
        Projectile=20
    }


    public ObjectPooler(EnemyView.Pool enemyViewPool, EnemyController.Factory enemyControllerFactory, HealthBar healthBar,
     EnemyPath enemyPath, Tower.Factory towerFactory, ProjectileController.Factory projectileControllerFactory, ProjectileView.Pool projectileViewPool) 
    {
        _enemyViewPool = enemyViewPool;
        _enemyControllerFactory = enemyControllerFactory;
        _healthBar = healthBar;
        _enemyPath = enemyPath;
        _towerFactory = towerFactory;
        _projectileControllerFactory =  projectileControllerFactory;
        _projectileViewPool = projectileViewPool;
    }



    public void SpawnObject(PoolType type, Vector3 position, Quaternion rotation, Transform parent){
        
        GameObject obj = new GameObject();
        
        switch (type)
        {
        case PoolType.Projectile: 
            var _projectileView = _projectileViewPool.Spawn();
            _projectileControllerFactory.Create(_projectileView);
            obj = _projectileView.gameObject;
            break;
        case PoolType.Enemy:
            var _enemyView = _enemyViewPool.Spawn();
            _enemyControllerFactory.Create(_enemyView, _healthBar, _enemyPath);
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
