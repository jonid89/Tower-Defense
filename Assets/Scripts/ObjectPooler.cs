using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler
{

    private EnemyView.Pool _enemyViewPool;
    private ProjectileView.Pool _projectileViewPool;
    private TowerView.Pool _towerViewPool;
    private ProjectileController.Factory _projectileControllerFactory;
    private EnemyController.Factory _enemyControllerFactory;
    private TowerController.Factory _towerControllerFactory;
    private LevelManagerController _levelManagerController;
    private EnemyPath _enemyPath;
    public enum PoolType
    {
        Tower=0,
        Enemy=10,
        Projectile=20
    }

    public ObjectPooler(
        EnemyView.Pool enemyViewPool, EnemyController.Factory enemyControllerFactory, 
        LevelManagerController levelManagerController,
        EnemyPath enemyPath, 
        TowerView.Pool towerViewPool, TowerController.Factory towerControllerFactory, 
        ProjectileView.Pool projectileViewPool, ProjectileController.Factory projectileControllerFactory) 
    {
        _enemyViewPool = enemyViewPool;
        _enemyControllerFactory = enemyControllerFactory;
        _levelManagerController = levelManagerController;
        _enemyPath = enemyPath;
        _towerViewPool = towerViewPool;
        _towerControllerFactory =  towerControllerFactory;
        _projectileControllerFactory =  projectileControllerFactory;
        _projectileViewPool = projectileViewPool;
    }

    public void SpawnObject(PoolType type, Vector3 position, Quaternion rotation, Transform parent){
        
        GameObject obj = new GameObject();

        switch (type)
        {
        case PoolType.Projectile: 
            var _projectileView = _projectileViewPool.Spawn();
            obj = _projectileView.gameObject;
            SetObjTransformValues();
            _projectileControllerFactory.Create(_projectileView);
            break;
        case PoolType.Enemy:
            var _enemyView = _enemyViewPool.Spawn();
            obj = _enemyView.gameObject;
            SetObjTransformValues();
            _enemyControllerFactory.Create(_enemyView, _levelManagerController, _enemyPath);
            break;
        case PoolType.Tower:
            var _towerView = _towerViewPool.Spawn();
            obj = _towerView.gameObject;
            SetObjTransformValues();
            _towerControllerFactory.Create(_towerView, this);
            break;
        }

        void SetObjTransformValues(){
            obj.SetActive(true);
            obj.transform.parent = parent;
            obj.transform.position = position;
            obj.transform.rotation = rotation;
        }

    }





}
