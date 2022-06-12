using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Tower : MonoBehaviour
{
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private GameObject _enemy;
    [SerializeField] private float _fireRate = 4f;
    
    private float _cooldown;
    ObjectPooler objectPooler;

    void Start()
    {
        objectPooler = ObjectPooler.Instance;
        float _cooldown = _fireRate;

    }

    void Update()
    {
        _cooldown -= Time.deltaTime;
        if(_cooldown <= 0){
            createProjectile();
            _cooldown = _fireRate;
        }
    }


    void createProjectile(){
        GameObject _projectile;
        _projectile = objectPooler.SpawnFromPool("Projectile",transform.position,Quaternion.identity,_enemy.transform.position, this.transform.parent);
        //,GameObject.FindGameObjectWithTag("Canvas").transform
                
    }
}


