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
    private Animator animator;
    private Vector2 startPoint = new Vector2();
    private Vector2 finalPoint = new Vector2();
    private Vector2 direction = new Vector2();



    [Inject]
    public void Construct (HealthBar playerHealth, EnemyPath enemyMoveController) {
        _playerHealth = playerHealth;
        _enemyMoveController = enemyMoveController;
    }


    public void OnObjectSpawn()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        startPoint = this.transform.position;

        waypointsPositions = _enemyMoveController.getWaypoints();
        float speed = Random.Range(averageSpeed-2,averageSpeed+2);
        path = this.transform.DOPath(waypointsPositions.ToArray(), averageSpeed, PathType.Linear, PathMode.Full3D)
            .SetEase(Ease.Linear)
            .OnWaypointChange(WalkAnimation)
            .OnStepComplete( () => EndReached());
    }

    public void WalkAnimation(int waypointIndex){
        finalPoint = waypointsPositions[waypointIndex];
        direction = (finalPoint - startPoint);
        direction.Normalize();
        

        if(direction == Vector2.up){
            animator.Play("GreySpiderWalkUp");
        }
        if(direction == Vector2.down){
            animator.Play("GreySpiderWalkDown");
        }
        if(direction == Vector2.left){
            animator.Play("GreySpiderWalkLeft");
        }
        if(direction == Vector2.right){
            animator.Play("GreySpiderWalkRight");
        }

        startPoint = finalPoint;
        
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
