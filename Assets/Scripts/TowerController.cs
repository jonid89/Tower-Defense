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
    private List<EnemyView> enemies = new List<EnemyView>();
    private EnemyView _target;
    TowerView _towerView;
    private Collider2D collider;

    public TowerController(TowerView towerView, ObjectPooler objectPooler){
      _objectPooler = objectPooler;
      _towerView = towerView;
      _towerView._towerController = this;
      _towerView.OnObjectSpawn();
      OnObjectSpawn();
    }

    public void OnObjectSpawn()
    { 
        collider =  _towerView.MyValue;
        
        collider.OnTriggerEnter2DAsObservable().Subscribe(other => ObjectEnteredPerimeter(other));
        collider.OnTriggerExit2DAsObservable().Subscribe(other => ObjectExitedPerimeter(other));

    } 
    
    private void FireIfEnemy()
    {
            GetCurrentTarget();
            if(_target != null)
            {
                _towerView.CheckCooldown(true, () => {createProjectile(); });
            }
    }

    void createProjectile(){
        _objectPooler.SpawnObject(ObjectPooler.PoolType.Projectile,_towerView.gameObject.transform.position,Quaternion.identity, _towerView.gameObject.transform);
    }

    public void ObjectEnteredPerimeter(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyView newEnemy = other.GetComponent<EnemyView>();
            enemies.Add(newEnemy);
            FireIfEnemy();
        }
    }

    public void ObjectExitedPerimeter(Collider2D other)
    {
        EnemyView enemy = other.GetComponent<EnemyView>();
        if (enemies.Contains(enemy))        
        {
            enemies.Remove(enemy);
            FireIfEnemy();
            if (_target == null){
                _towerView._hasTarget = false;
            }
        }        
    }

    public EnemyView getTarget(){
        return _target;
    }

    private void GetCurrentTarget()
    {
        _target = enemies.Count > 0 ? enemies[0] : null;
    }

    public class Factory : PlaceholderFactory<TowerView, ObjectPooler, TowerController>
    {
    }

}
