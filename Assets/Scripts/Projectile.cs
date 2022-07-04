using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Zenject;

public class Projectile : MonoBehaviour, IPooledObject
{
    [SerializeField] float speed = 10f;
    [SerializeField] int damage = 5;
    private Enemy enemy = null;
    private Vector3 enemyPosition;
    public void OnObjectSpawn()
    {
        fireAtEnemy();
    }



    private void fireAtEnemy(){
        enemy = this.transform.parent.GetComponent<Tower>().getTarget();
        enemyPosition = enemy.transform.position;
        
        this.transform.DOMove(enemyPosition,speed)
            .SetEase(Ease.Linear)
            .OnStepComplete( () => 
                {
                    enemy.GetDamage(damage);
                    this.gameObject.SetActive(false);
                }
                );

    }

    public class Factory : PlaceholderFactory<Projectile>
    {
    }
}
