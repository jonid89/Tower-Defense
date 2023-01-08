using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UniRx;
using UniRx.Triggers;


public class TowerView : MonoBehaviour, IPooledObject
{
    [SerializeField] public float _fireRate = 4f;
    [SerializeField] private Collider2D _collider;
    public Collider2D MyCollider{
        get { return _collider;}
    }
    public bool _hasTarget;
    private float _cooldown;
    private Action _timerCallback;
    public TowerController _towerController;

    public void OnObjectSpawn()
    {
        _hasTarget = false;
        _cooldown = 0f;
    }

    void Update()
    {

        if (_cooldown > 0f ) _cooldown -= Time.deltaTime;
        else if  (_hasTarget){
            _timerCallback();
            _cooldown = _fireRate;
        }
    }

    public void CheckCooldown(bool hasTarget, Action timerCallback){
        this._hasTarget = hasTarget;
        this._timerCallback = timerCallback;
    }

    public class Pool : MemoryPool<TowerView>
    {
    }
    
}


