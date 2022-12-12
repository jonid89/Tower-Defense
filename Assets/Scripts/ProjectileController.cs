using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Zenject;

public class ProjectileController : IPooledObject
{
    private EnemyView enemy = null;
    private Vector3 enemyPosition;

    ProjectileView _projectileView;
    
    
    public ProjectileController(ProjectileView projectileView) {
        _projectileView = projectileView;
        fireAtEnemy();
        _projectileView.OnObjectSpawn();
    }

 
    public void OnObjectSpawn()
    {
        
    }

    public void fireAtEnemy(){
        //Debug.Log(_projectileView.transform.parent);
        enemy = _projectileView.transform.parent.GetComponent<Tower>().getTarget();
        enemyPosition = enemy.GetComponent<Transform>().position;
        //Debug.Log(enemyPosition);
        //DOTween.To(()=> myVector, x=> myVector = x, new Vector3(3,4,8), 1);
        Vector3 projectilePos = _projectileView.transform.position;
        DOTween.To(()=> projectilePos, x=>_projectileView.transform.position=x, enemyPosition,_projectileView.speed)
        //_projectileView.transform.DOMove(enemyPosition,_projectileView.speed)
            .SetEase(Ease.Linear)
            .OnStepComplete( () => 
                {
                    enemy._enemyController.GetDamage(_projectileView.damage);
                    _projectileView.gameObject.SetActive(false);
                }
                );

    }

    public class Factory : PlaceholderFactory<ProjectileView, ProjectileController>
    {
    }
}
