using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Zenject;
using UniRx;


public class EnemyController : IPooledObject, IDisposable
{
    private int _currentHealth;
    private float _timeToFinishPath;
    EnemyView _enemyView;
    EnemyView.Pool _enemyViewPool;
    EnemyPath _enemyPath;
    private Transform _transform;
    LevelManagerController _levelManagerController;
    private List<Vector3> _waypointsPositions = new List<Vector3>();
    private Tweener _path;
    private Vector2 _startPoint = new Vector2();
    private Vector2 _finalPoint = new Vector2();
    private Vector2 _direction = new Vector2();

    public EnemyController(EnemyView enemyView, EnemyView.Pool enemyViewPool, LevelManagerController levelManagerController, EnemyPath enemyPath) {
        _enemyView = enemyView;
        _enemyViewPool = enemyViewPool;
        _levelManagerController = levelManagerController;
        _enemyPath = enemyPath;
        _enemyView._enemyController = this;
        _currentHealth = _enemyView._maxHealth;
        _enemyView._enemyPath = _enemyPath;
        _timeToFinishPath = _enemyView._timeToFinishPath;
        _enemyView.OnObjectSpawn();
        _transform = _enemyView.MyTransform;
        OnObjectSpawn();
    }

    public void OnObjectSpawn()
    {
        _startPoint = _transform.position;
        _waypointsPositions = _enemyPath.getWaypoints();
        
        float speed = UnityEngine.Random.Range(_timeToFinishPath-2,_timeToFinishPath+2);

        _path = _transform.DOPath(_waypointsPositions.ToArray(), speed, PathType.Linear, PathMode.Full3D)
            .SetEase(Ease.Linear)
            .OnWaypointChange(SetSprites)
            .OnStepComplete( () => EndReached());
    }

    public void SetSprites(int waypointIndex){
        _finalPoint = _waypointsPositions[waypointIndex];
        _direction = (_finalPoint - _startPoint);
        _direction.Normalize();

        if(_direction == Vector2.up){
            _enemyView._currentSprites = _enemyView._spritesWalkUp;
        }
        if(_direction == Vector2.down){
            _enemyView._currentSprites = _enemyView._spritesWalkDown;
        }
        if(_direction == Vector2.left){
            _enemyView._currentSprites = _enemyView._spritesWalkLeft;
        }
        if(_direction == Vector2.right){
            _enemyView._currentSprites = _enemyView._spritesWalkRight;
        }
        _startPoint = _finalPoint;
    }

    public void GetDamage(int damage)
    {
        if(_currentHealth > 0){
            _currentHealth -= damage;
            if (_currentHealth <= 0 )
            {
                _enemyView._currentSprites = _enemyView._spriteDead;
                _path.Kill();
                _enemyView._spriteRenderer.DOFade(0,2f).OnComplete( () => EndEnemy());
            }
        }
    }

    public void EndReached(){
        _levelManagerController.DamagePlayer();
        EndEnemy();
    }

    public void EndEnemy(){
        _enemyView.gameObject.SetActive(false);
        //Debug.Log($"{_enemyView.gameObject.name} Deactivated");
        _levelManagerController.EnemyDestroyed();
        //Debug.Log($"{_enemyView.gameObject.name} Destroyed");
        _path.Restart();
        _enemyViewPool.Despawn(_enemyView);
        //Debug.Log($"{_enemyView.gameObject.name} Despawned");
        Dispose();
    }

    public void Dispose(){
        //Debug.Log("Dispose() called");
    }

    public class Factory : PlaceholderFactory<EnemyView, EnemyView.Pool, EnemyPath, EnemyController>
    {
    }
}
