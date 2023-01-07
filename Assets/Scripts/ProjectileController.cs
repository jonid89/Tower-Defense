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
    }

    public void OnObjectSpawn()
    {
        
    }

    public void fireAtEnemy(){
        enemy = _projectileView.transform.parent.GetComponent<TowerView>()._towerController.getTarget();
        enemyPosition = enemy.GetComponent<Transform>().position;
        Vector3 projectilePos = _projectileView.transform.position;
        DOTween.To(()=> projectilePos, x=>_projectileView.transform.position=x, enemyPosition,_projectileView.speed)
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
