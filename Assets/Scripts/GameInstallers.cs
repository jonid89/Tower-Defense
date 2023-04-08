using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Zenject;

public class GameInstallers : MonoInstaller
{
    [SerializeField]
    private EnemyPath _enemyPath;
    
    [SerializeField]
    private LevelManagerView _levelManagerView;

    [SerializeField]
    private HealthBarView _healthbar;
    [SerializeField]
    private EnemySpawnerView _enemySpawner;
    
    [SerializeField]
    private GameObject _enemyPrefab;
    
    [SerializeField]
    private GameObject _towerPrefab;

    [SerializeField]
    private GameObject _projectilePrefab;
    
    public override void InstallBindings()
    {
        
        Container.Bind<EnemyPath>().FromInstance(_enemyPath).AsSingle().NonLazy();
        
        Container.Bind<HealthBarView>().FromInstance(_healthbar).AsSingle().NonLazy();
        Container.Bind<HealthBarController>().AsSingle().NonLazy();
        
        Container.Bind<LevelManagerView>().FromInstance(_levelManagerView).AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<LevelManagerController>().AsSingle().NonLazy();

        Container.Bind<EnemySpawnerView>().FromInstance(_enemySpawner).AsSingle().NonLazy();
        Container.Bind<EnemySpawnerController>().AsSingle().NonLazy();
        
        Container.BindInterfacesAndSelfTo<ObjectPooler>().AsSingle().NonLazy();
        
        Container.BindFactory<EnemyView, EnemyView.Pool, EnemyPath, EnemyController, EnemyController.Factory>();
        Container.BindMemoryPool<EnemyView, EnemyView.Pool>().FromComponentInNewPrefab(_enemyPrefab).NonLazy();

        Container.BindFactory<TowerView, ObjectPooler, TowerController, TowerController.Factory>();
        Container.BindMemoryPool<TowerView, TowerView.Pool>().FromComponentInNewPrefab(_towerPrefab).NonLazy();
        
        Container.BindFactory<ProjectileView, ProjectileController, ProjectileController.Factory>();
        Container.BindMemoryPool<ProjectileView, ProjectileView.Pool>().FromComponentInNewPrefab(_projectilePrefab).NonLazy();

    }
    

}
