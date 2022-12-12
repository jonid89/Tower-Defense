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
    LevelManager _levelManager;
    private List<Vector3> waypointsPositions = new List<Vector3>();
    private Tweener path;
    private Animator animator;
    private Vector2 startPoint = new Vector2();
    private Vector2 finalPoint = new Vector2();
    private Vector2 direction = new Vector2();


    public EnemyController(EnemyView enemyView, LevelManager levelManager, EnemyPath enemyPath) {
        _enemyView = enemyView;
        _levelManager = levelManager;
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
        _levelManager.DamagePlayer();
        _enemyView.EndEnemy();
        /*gameObject.SetActive(false);
        path.Restart();
        path.Kill();*/
    }


    public void GetDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0 )
        {
            _enemyView.EndEnemy();
            /*path.Restart();
            path.Kill();
            _enemyView.gameObject.SetActive(false);*/
        }
    }

    public class Factory : PlaceholderFactory<EnemyView, LevelManager, EnemyPath, EnemyController>
    {
    }
}
