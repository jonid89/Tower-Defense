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
    private LevelManager _levelManager;

  [SerializeField]
    private HealthBar _healthbar;
    
    [SerializeField]
    private GameObject EnemyPrefab;
    
    [SerializeField]
    private GameObject TowerPrefab;

    [SerializeField]
    private GameObject ProjectilePrefab;
    
    public override void InstallBindings()
    {
        
        Container.Bind<EnemyPath>().FromInstance(_enemyPath).AsSingle().NonLazy();
        Container.Bind<HealthBar>().FromInstance(_healthbar).AsSingle().NonLazy();
        Container.Bind<LevelManager>().FromInstance(_levelManager).AsSingle().NonLazy();
        
        Container.BindInterfacesAndSelfTo<ObjectPooler>().AsSingle().NonLazy();
        
        Container.BindFactory<EnemyView, LevelManager, EnemyPath, EnemyController, EnemyController.Factory>();
        Container.BindMemoryPool<EnemyView, EnemyView.Pool>().FromComponentInNewPrefab(EnemyPrefab).NonLazy();

        Container.BindFactory<Tower, Tower.Factory>().FromComponentInNewPrefab(TowerPrefab);
        
        Container.BindFactory<ProjectileView, ProjectileController, ProjectileController.Factory>();
        Container.BindMemoryPool<ProjectileView, ProjectileView.Pool>().FromComponentInNewPrefab(ProjectilePrefab).NonLazy();


    }


    

}
