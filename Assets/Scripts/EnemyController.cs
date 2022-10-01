using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Zenject;

public class EnemyController : MonoBehaviour, IPooledObject
{
    [SerializeField] private float averageSpeed;
    [SerializeField] int maxHealth;
    private int currentHealth;
    EnemyPath _enemyMoveController;
    HealthBar _playerHealth;
    private List<Vector3> waypointsPositions = new List<Vector3>();
    private Tweener path;


    [Inject]
    public void Construct (HealthBar playerHealth, EnemyPath enemyMoveController) {
        _playerHealth = playerHealth;
        _enemyMoveController = enemyMoveController;
    }


    public void OnObjectSpawn()
    {
        currentHealth = maxHealth;

        waypointsPositions = _enemyMoveController.getWaypoints();
        float speed = Random.Range(averageSpeed-2,averageSpeed+2);
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

    public class Factory : PlaceholderFactory<HealthBar, EnemyPath, EnemyController>
    {
    }
}
