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

    private GameObject TowerPrefab;
    
    private GameObject ProjectilePrefab;
    
    public override void InstallBindings()
    {
        /*Container.Bind<GameOverPanel>().FromInstance(_gameOverPanel).AsSingle().NonLazy();
        Container.Bind<LevelWonPanel>().FromInstance(_levelWonPanel).AsSingle().NonLazy();*/
        Container.BindInterfacesTo<ObjectPooler>();
        Container.Bind<EnemyMoveController>().FromInstance(_enemyMoveController).AsSingle().NonLazy();
        Container.Bind<HealthBar>().FromInstance(_healthBar).AsSingle().NonLazy();
        Container.Bind<ObjectPooler>().FromInstance(_objectPooler).AsSingle().NonLazy();
        Container.BindFactory<Enemy, Enemy.Factory>().FromComponentInNewPrefab(EnemyPrefab);
        Container.BindFactory<Tower, Tower.Factory>().FromComponentInNewPrefab(TowerPrefab);
        Container.BindFactory<Projectile, Projectile.Factory>().FromComponentInNewPrefab(ProjectilePrefab);
        
    }


    

}
