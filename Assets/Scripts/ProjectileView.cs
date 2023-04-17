using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Zenject;

public class ProjectileView : MonoBehaviour, IPooledObject
{

    public void OnObjectSpawn()
    {
    }

    public class Pool : MemoryPool<ProjectileView>
    {
        
    }
}
