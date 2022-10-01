using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Zenject;

public class Enemy : MonoBehaviour, IPooledObject
{
    [SerializeField] private float averageSpeed = 30f;
    [SerializeField] int maxHealth = 100;
    private int currentHealth;
    EnemyMoveController _enemyMoveController;
    HealthBar _playerHealth;
    private List<Vector3> waypointsPositions = new List<Vector3>();
    private Tweener path;


    [Inject]
    public void Construct (HealthBar playerHealth, EnemyMoveController enemyMoveController) {
        _playerHealth = playerHealth;
        _enemyMoveController = enemyMoveController;
    }


    public void OnObjectSpawn()
    {
        currentHealth = maxHealth;

        waypointsPositions = _enemyMoveController.getWaypoints();
        averageSpeed = Random.Range(averageSpeed-5,averageSpeed+5);
        path = this.transform.DOPath(waypointsPositions.ToArray(), averageSpeed, PathType.Linear, PathMode.Full3D)
            .SetEase(Ease.Linear)
            .OnStepComplete( () => EndReached());
    }


    public void EndReached(){
        _playerHealth.DamagePlayer();
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

    public class Factory : PlaceholderFactory<HealthBar, EnemyMoveController, Enemy>
    {
    }
}
