using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Zenject;
using UniRx;
using UniRx.Triggers;

public class TowerController : IPooledObject
{
    private float _cooldown;
    ObjectPooler _objectPooler;
    private List<EnemyState> _enemies = new List<EnemyState>();
    private EnemyState _target;
    TowerView _towerView;
    private Collider2D _collider;
    private TowerConfig _towerConfig;

    public TowerController(TowerView towerView, ObjectPooler objectPooler){
      _objectPooler = objectPooler;
      _towerView = towerView;
      _towerView._towerController = this;
      _towerView.OnObjectSpawn();
      OnObjectSpawn();
    }

    public void OnObjectSpawn()
    { 
        _collider =  _towerView.MyCollider;
        _collider.OnTriggerEnter2DAsObservable().Subscribe(other => ObjectEnteredPerimeter(other));
        _collider.OnTriggerExit2DAsObservable().Subscribe(other => ObjectExitedPerimeter(other));
        
        _towerConfig = _towerView.GetTowerConfig;
    } 
    
    private void FireIfEnemy()
    {
        GetCurrentTarget();
        
        if(_target != null)
        {
            _towerView.CheckCooldown(true, () => {GetCurrentTarget(); createProjectile(); });
        }
        
    }

    void createProjectile(){
        _objectPooler.SpawnObject(ObjectPooler.PoolType.Projectile,_towerView.gameObject.transform.position,Quaternion.identity, _towerView.gameObject.transform);
    }

    public void ObjectEnteredPerimeter(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyState newEnemy = other.GetComponent<EnemyState>();
            _enemies.Add(newEnemy);
            FireIfEnemy();
        }
    }

    public void ObjectExitedPerimeter(Collider2D other)
    {
        EnemyState enemy = other.GetComponent<EnemyState>();
        if (_enemies.Contains(enemy))        
        {
            _enemies.Remove(enemy);
            FireIfEnemy();
            if (_target == null){
                _towerView._hasTarget = false;
            }
        }        
    }

    public EnemyState getTarget(){
        return _target;
    }

    private void GetCurrentTarget()
    {
        while(_enemies.Count > 0 && _enemies[0] != null && _enemies[0].IsDead == true){
            _enemies.Remove(_enemies[0]);
            _towerView._hasTarget = false;
        }
        _target = _enemies.Count > 0 ? _enemies[0] : null;

    }

    public class Factory : PlaceholderFactory<TowerView, ObjectPooler, TowerController>
    {
    }

}
