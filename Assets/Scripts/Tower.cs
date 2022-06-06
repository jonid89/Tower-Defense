using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Tower : MonoBehaviour
{
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private GameObject _enemy;
    [SerializeField] private float _fireRate = 4f;
    
    private float _counter;

    void Start()
    {
        float _counter = _fireRate;
    }


    void Update()
    {
        _counter -= Time.deltaTime;
        if(_counter <= 0){
            createProjectile();
            _counter = _fireRate;
        }
    }


    void createProjectile(){
        GameObject _projectile;
        _projectile = Instantiate(_projectilePrefab,transform.position,Quaternion.identity);
        //,GameObject.FindGameObjectWithTag("Canvas").transform
        _projectile.transform.SetParent(this.transform.parent);
        _projectile.GetComponent<Projectile>().getEnemy(_enemy.transform.position);
    }
}


