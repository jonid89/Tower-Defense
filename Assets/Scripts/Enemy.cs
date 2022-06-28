using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Enemy : MonoBehaviour, IPooledObject
{
    [SerializeField] private float averageSpeed = 30f;
    [SerializeField] int maxHealth = 100;
    private int currentHealth;
    EnemyMoveController enemyMoveController;
    private List<Vector3> waypointsPositions = new List<Vector3>();
    private Tweener path;
    HealthBar playerHealth;


    public void OnObjectSpawn()
    {
        enemyMoveController = EnemyMoveController.Instance;
        playerHealth = HealthBar.Instance;
        currentHealth = maxHealth;
        

        waypointsPositions = enemyMoveController.getWaypoints();
        averageSpeed = Random.Range(averageSpeed-5,averageSpeed+5);
        path = this.transform.DOPath(waypointsPositions.ToArray(), averageSpeed, PathType.Linear, PathMode.Full3D)
            .SetEase(Ease.Linear)
            .OnStepComplete( () => EndReached());
    }


    public void EndReached(){
        playerHealth.DamagePlayer();
        this.gameObject.SetActive(false);
        
    }


    public void GetDamage(int damage)
    {
        Debug.Log(damage);
        currentHealth -= damage;
        Debug.Log(currentHealth);
        if (currentHealth <= 0 )
        {
            this.gameObject.SetActive(false);
            path.Restart();
            path.Kill();
        }
    }


}
