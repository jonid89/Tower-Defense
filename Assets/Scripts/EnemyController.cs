using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Zenject;
using UniRx;


public class EnemyController : IPooledObject
{
    [SerializeField] private float averageSpeed;
    private int currentHealth;
    EnemyView _enemyView;
    EnemyPath _enemyPath;
    LevelManagerController _levelManagerController;
    private List<Vector3> waypointsPositions = new List<Vector3>();
    private Tweener path;
    private Animator animator;
    private Vector2 startPoint = new Vector2();
    private Vector2 finalPoint = new Vector2();
    private Vector2 direction = new Vector2();


    public EnemyController(EnemyView enemyView, LevelManagerController levelManagerController, EnemyPath enemyPath) {
        _enemyView = enemyView;
        _levelManagerController = levelManagerController;
        _enemyPath = enemyPath;
        _enemyView._enemyController = this;
        currentHealth = _enemyView.maxHealth;
        _enemyView._enemyPath = _enemyPath;
        _enemyView.OnObjectSpawn();
        path = _enemyView._path;
    }


    public void OnObjectSpawn()
    {
        
    }


    public void EndReached(){
        _levelManagerController.DamagePlayer();
        _enemyView.EndEnemy();
    }


    public void GetDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0 )
        {
            _enemyView.EndEnemy();
        }
    }

    public class Factory : PlaceholderFactory<EnemyView, LevelManagerController, EnemyPath, EnemyController>
    {
    }
}
