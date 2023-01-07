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
    public TowerController _towerController;

    public bool _hasTarget;
    
    public Collider2D MyValue{
        get { return _collider;}
    }

    private float cooldown;
    private Action timerCallback;

    public void OnObjectSpawn()
    {
        _hasTarget = false;
        cooldown = 0f;
    }

    void Update()
    {

        if (cooldown > 0f ) cooldown -= Time.deltaTime;
        else if  (_hasTarget){
            timerCallback();
            cooldown = _fireRate;
        }
    }

    public void CheckCooldown(bool hasTarget, Action timerCallback){
        this._hasTarget = hasTarget;
        this.timerCallback = timerCallback;
    }


    public class Pool : MemoryPool<TowerView>
    {

    }
    
}


