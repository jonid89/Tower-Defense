using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Zenject;

public class ProjectileView : MonoBehaviour, IPooledObject
{
    [SerializeField] public float speed = 10f;
    [SerializeField] public int damage = 5;
    ProjectileController _projectileController;

    public void OnObjectSpawn()
    {
        //Debug.Log("projectileSpawned");
        //_projectileController.fireAtEnemy();
    }


        public class Pool : MemoryPool<ProjectileView>
    {
        
    }
}
