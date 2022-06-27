using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Tower : MonoBehaviour
{
    [SerializeField] private float fireRate = 4f;
    private float cooldown;
    ObjectPooler objectPooler;

    private List<Enemy> enemies = new List<Enemy>();
    private Enemy target;

    void Start()
    {
        objectPooler = ObjectPooler.Instance;
        float _cooldown = fireRate;        
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
        GameObject projectile = objectPooler.SpawnFromPool(ObjectPooler.PoolType.Projectile,transform.position,Quaternion.identity, this.transform);
    }

    public Enemy getTarget(){
        if(target!=null){
            return target;
        }
        else return null;
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
        if (enemies.Count <= 0)
        {
            target = null;
            return;
        }
        
        target = enemies[0];
    }

}


