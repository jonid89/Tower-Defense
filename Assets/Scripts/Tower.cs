using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;


public class Tower : MonoBehaviour
{
    [SerializeField] private float fireRate = 4f;
    private float cooldown;
    ObjectPooler _objectPooler;

    private List<EnemyView> enemies = new List<EnemyView>();
    private EnemyView target;

    void Start()
    {
        float _cooldown = fireRate;        
    }

    
    [Inject]
    public void Construct (ObjectPooler objectPooler) {
        _objectPooler = objectPooler;
    }
    

    void Update()
    {
        GetCurrentTarget();
        cooldown -= Time.deltaTime;
        if(cooldown <= 0 && target != null){
            createProjectile();
            cooldown = fireRate;
        }
    }

    void createProjectile(){
        _objectPooler.SpawnObject(ObjectPooler.PoolType.Projectile,transform.position,Quaternion.identity, this.transform);
    }

    public EnemyView getTarget(){
        return target;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyView newEnemy = other.GetComponent<EnemyView>();
            enemies.Add(newEnemy);
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        EnemyView enemy = other.GetComponent<EnemyView>();
        if (enemies.Contains(enemy))        
        {
            enemies.Remove(enemy);
        }
        
    }

    private void GetCurrentTarget()
    {
        target = enemies.Count > 0 ? enemies[0] : null;
    }

    public class Factory : PlaceholderFactory<Tower>
    {
    }
    
}


