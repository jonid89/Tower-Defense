using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Enemy : MonoBehaviour, IPooledObject
{

    [SerializeField] private float speed = 20f;
    [SerializeField] int maxHealth = 100;
    private int currentHealth = 100;
    EnemyMoveController enemyMoveController;
    private List<Vector3> waypointsPositions = new List<Vector3>();


    public void OnObjectSpawn()
    {
        enemyMoveController = EnemyMoveController.Instance;

        waypointsPositions = enemyMoveController.getWaypoints();
        speed = Random.Range(15,25);
        this.transform.DOPath(waypointsPositions.ToArray(), speed, PathType.Linear, PathMode.Full3D).SetEase(Ease.Linear).OnStepComplete( () => this.gameObject.SetActive(false));
    }

    void Start()
    {

    }

    public void GetDamage(int damage)
    {
        Debug.Log(damage);
        currentHealth -= damage;
        Debug.Log(currentHealth);
        if (currentHealth <= 0 )
        {
            this.gameObject.SetActive(false);
        }
    }


    void Update()
    {
        
    }
}
