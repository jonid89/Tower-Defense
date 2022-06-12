using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Projectile : MonoBehaviour, IPooledObject
{
    [SerializeField] float _speed = 10f;
    private Vector3 _enemyPosition;
    public void OnObjectSpawn()
    {
        this.transform.DOMove(_enemyPosition,_speed).SetEase(Ease.Linear).OnStepComplete( () => this.gameObject.SetActive(false) );
    }

    void Update()
    {
 
    }

    public void getEnemy(Vector3 _enemyPos){
        _enemyPosition = _enemyPos;
    }
}
