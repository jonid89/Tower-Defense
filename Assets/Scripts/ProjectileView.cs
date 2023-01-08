using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Zenject;

public class ProjectileView : MonoBehaviour, IPooledObject
{
    [SerializeField] public float _speed = 1f;
    [SerializeField] public int _damage = 5;

    public void OnObjectSpawn()
    {
    }

    public class Pool : MemoryPool<ProjectileView>
    {
        
    }
}
