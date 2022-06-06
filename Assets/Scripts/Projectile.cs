using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Projectile : MonoBehaviour
{
    [SerializeField] float _speed = 10f;
    private Vector3 _enemyPosition;
    void Start()
    {
        this.transform.DOMove(_enemyPosition,_speed).SetEase(Ease.Linear).OnStepComplete( () => Destroy( transform.gameObject ) );
    }


    void Update()
    {
        
    }

    public void getEnemy(Vector3 _enemyPos){
        _enemyPosition = _enemyPos;
    }
}
