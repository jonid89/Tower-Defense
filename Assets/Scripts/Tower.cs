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

    private List<Enemy> enemies = new List<Enemy>();
    private Enemy target;

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
        GameObject projectile = _objectPooler.SpawnFromPool(ObjectPooler.PoolType.Projectile,transform.position,Quaternion.identity, this.transform);
    }

    public Enemy getTarget(){
        return target;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy newEnemy = other.GetComponent<Enemy>();
            enemies.Add(newEnemy);
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
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


