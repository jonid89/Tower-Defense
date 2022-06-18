using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Tower : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private float fireRate = 4f;
    
    private float cooldown;
    ObjectPooler objectPooler;

    void Start()
    {
        objectPooler = ObjectPooler.Instance;
        float _cooldown = fireRate;
        enemy = GameObject.Find("Enemy");
        
    }

    void Update()
    {
        cooldown -= Time.deltaTime;
        if(cooldown <= 0){
            createProjectile();
            cooldown = fireRate;
        }
    }


    void createProjectile(){
        GameObject projectile = objectPooler.SpawnFromPool("Projectile",transform.position,Quaternion.identity, this.transform.parent, enemy.transform.position);
    }
}


