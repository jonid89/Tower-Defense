using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class EnemyState : MonoBehaviour, IPooledObject
{
    public EnemyController _enemyController;
    private ReactiveProperty<bool> _isDead;

    public void Awake(){
        _isDead = new ReactiveProperty<bool>(false).AddTo(this);
    }
    public bool IsDead => _isDead.Value;
    public void SetDead(bool dead){
        _isDead.Value = dead;
    }

    public void OnObjectSpawn(){
        _isDead.Value = false;
    }

}
