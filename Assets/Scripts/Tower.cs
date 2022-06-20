using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Tower : MonoBehaviour
{
    [SerializeField] private float fireRate = 4f;

    private GameObject enemy;    
    private float cooldown;
    ObjectPooler objectPooler;

    void Start()
    {
        objectPooler = ObjectPooler.Instance;
        float _cooldown = fireRate;        
    }

    void Update()
    {
        cooldown -= Time.deltaTime;
        if(cooldown <= 0 && enemy != null){
            createProjectile();
            cooldown = fireRate;
        }
    }

    void createProjectile(){
        GameObject projectile = objectPooler.SpawnFromPool("Projectile",transform.position,Quaternion.identity, this.transform.parent, enemy.transform.position);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(enemy == null)
        {
            enemy = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        enemy = null;
    }

}


