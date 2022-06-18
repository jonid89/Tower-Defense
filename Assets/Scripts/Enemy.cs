using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Enemy : MonoBehaviour, IPooledObject
{

    [SerializeField] private float speed = 10f;
    EnemyMoveController enemyMoveController;
    private List<Vector3> waypointsPositions = new List<Vector3>();


    public void OnObjectSpawn()
    {
        enemyMoveController = EnemyMoveController.Instance;

        waypointsPositions = enemyMoveController.getWaypoints();

        this.transform.DOPath(waypointsPositions.ToArray(), speed, PathType.Linear, PathMode.Full3D).SetEase(Ease.Linear).OnStepComplete( () => this.gameObject.SetActive(false));
    }

    void Start()
    {

    }



    void Update()
    {
        
    }
}
