using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Projectile : MonoBehaviour, IPooledObject
{
    [SerializeField] float speed = 10f;
    private Vector3 enemyPosition;
    public void OnObjectSpawn()
    {
        this.transform.DOMove(enemyPosition,speed).SetEase(Ease.Linear).OnStepComplete( () => this.gameObject.SetActive(false) );
    }

    void Update()
    {
 
    }

    public void getEnemy(Vector3 enemyPos){
        enemyPosition = enemyPos;
    }
}
