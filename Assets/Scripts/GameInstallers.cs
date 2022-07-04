using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Zenject;

public class GameInstallers : MonoInstaller
{
    [SerializeField]
    private EnemyMoveController _enemyMoveController;
    
    [SerializeField]
    private HealthBar _healthBar;
    
    [SerializeField]
    private ObjectPooler _objectPooler;
    
    private GameObject EnemyPrefab;
    
    public override void InstallBindings()
    {
        Debug.Log(_enemyMoveController);
        Debug.Log(_healthBar);
        Container.Bind<EnemyMoveController>().FromInstance(_enemyMoveController).AsSingle().NonLazy();
        Container.Bind<HealthBar>().FromInstance(_healthBar).AsSingle().NonLazy();
        Container.Bind<ObjectPooler>().FromInstance(_objectPooler).AsSingle().NonLazy();
        // Container.Bind<Enemy>().AsTransient();
        Container.BindFactory<Enemy, Enemy.Factory>().FromComponentInNewPrefab(EnemyPrefab);
        // Container.Bind<ObjectPooler>().AsSingle().NonLazy();

        //Container.Bind<IEnemy>().To<Enemy>().FromFactory<EnemyFactory>();

    }


    

}
