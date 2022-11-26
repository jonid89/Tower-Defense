using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Zenject;

public class ProjectileController
{
    private EnemyView enemy = null;
    private Vector3 enemyPosition;

    ProjectileView _projectileView;
    
    
    public ProjectileController(ProjectileView projectileView) {
        _projectileView = projectileView;
    }

 


    public void fireAtEnemy(){
        enemy = _projectileView.transform.parent.GetComponent<Tower>().getTarget();
        enemyPosition = enemy.GetComponent<Transform>().position;
        
        _projectileView.transform.DOMove(enemyPosition,_projectileView.speed)
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
