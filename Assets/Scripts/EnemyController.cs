using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Zenject;


public class EnemyController : IPooledObject
{
    [SerializeField] private float averageSpeed;
    [SerializeField] int maxHealth;
    private int currentHealth;
    EnemyView _enemyView;
    EnemyPath _enemyPath;
    HealthBar _playerHealth;
    private List<Vector3> waypointsPositions = new List<Vector3>();
    private Tweener path;
    private Animator animator;
    private Vector2 startPoint = new Vector2();
    private Vector2 finalPoint = new Vector2();
    private Vector2 direction = new Vector2();


    public EnemyController(EnemyView enemyView, HealthBar playerHealth, EnemyPath enemyPath) {
        _enemyView = enemyView;
        _playerHealth = playerHealth;
        _enemyPath = enemyPath;

        currentHealth = maxHealth;
        _enemyView._enemyController = this;
        _enemyView._enemyPath = _enemyPath;
        _enemyView.OnObjectSpawn();
    }


    public void OnObjectSpawn()
    {
        
    }


    public void EndReached(){
        _playerHealth.DamagePlayer();
        _enemyView.gameObject.SetActive(false);
    }


    public void GetDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0 )
        {
            _enemyView.gameObject.SetActive(false);
            path.Restart();
            path.Kill();
        }
    }

    public Transform GetTransform(){
        return _enemyView.GetComponent<Transform>();
    }

    public class Factory : PlaceholderFactory<EnemyView, HealthBar, EnemyPath, EnemyController>
    {
    }
}
