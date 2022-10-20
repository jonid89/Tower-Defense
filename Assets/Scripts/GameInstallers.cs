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
    private HealthBar _healthBar;

    
    [SerializeField]
    private GameObject EnemyPrefab;
    
    [SerializeField]
    private GameObject TowerPrefab;

    [SerializeField]
    private GameObject ProjectilePrefab;
    
    public override void InstallBindings()
    {
        
        Container.Bind<EnemyPath>().FromInstance(_enemyPath).AsSingle().NonLazy();
        Container.Bind<HealthBar>().FromInstance(_healthBar).AsSingle().NonLazy();
        
        Container.BindInterfacesAndSelfTo<ObjectPooler>().AsSingle().NonLazy();
        
        Container.BindFactory<EnemyView, HealthBar, EnemyPath, EnemyController, EnemyController.Factory>();
        Container.BindMemoryPool<EnemyView, EnemyView.Pool>().FromComponentInNewPrefab(EnemyPrefab).NonLazy();

        Container.BindFactory<Tower, Tower.Factory>().FromComponentInNewPrefab(TowerPrefab);
        Container.BindFactory<Projectile, Projectile.Factory>().FromComponentInNewPrefab(ProjectilePrefab);
        
    }


    

}
