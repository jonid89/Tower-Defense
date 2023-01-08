using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Zenject;
using UniRx;


public class EnemyController : IPooledObject
{
    [SerializeField] private float averageSpeed;
    private int _currentHealth;
    EnemyView _enemyView;
    EnemyPath _enemyPath;
    LevelManagerController _levelManagerController;
    private List<Vector3> _waypointsPositions = new List<Vector3>();
    private Tweener _path;
    private Animator _animator;
    private Vector2 _startPoint = new Vector2();
    private Vector2 _finalPoint = new Vector2();
    private Vector2 _direction = new Vector2();


    public EnemyController(EnemyView enemyView, LevelManagerController levelManagerController, EnemyPath enemyPath) {
        _enemyView = enemyView;
        _levelManagerController = levelManagerController;
        _enemyPath = enemyPath;
        _enemyView._enemyController = this;
        _currentHealth = _enemyView._maxHealth;
        _enemyView._enemyPath = _enemyPath;
        _enemyView.OnObjectSpawn();
        _path = _enemyView._path;
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
        _currentHealth -= damage;
        if (_currentHealth <= 0 )
        {
            _enemyView.EndEnemy();
        }
    }

    public class Factory : PlaceholderFactory<EnemyView, LevelManagerController, EnemyPath, EnemyController>
    {
    }
}
