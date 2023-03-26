using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Zenject;

public class ProjectileController : IPooledObject
{
    private EnemyView _enemy = null;
    private Vector3 _enemyPosition;

    ProjectileView _projectileView;
    
    
    public ProjectileController(ProjectileView projectileView) {
        _projectileView = projectileView;
        fireAtEnemy();
    }

    public void OnObjectSpawn()
    {
        
    }

    public void fireAtEnemy(){
        _enemy = _projectileView.transform.parent.GetComponent<TowerView>()._towerController.getTarget();
        _enemyPosition = _enemy.GetComponent<Transform>().position;
        Vector3 projectilePos = _projectileView.transform.position;
        DOTween.To(()=> projectilePos, x=>_projectileView.transform.position=x, _enemyPosition,_projectileView._speed)
            .SetEase(Ease.Linear)
            .OnStepComplete( () => 
                {
                    _enemy._enemyController.GetDamage(_projectileView._damage);
                    _projectileView.gameObject.SetActive(false);
                }
            );

    }

    public class Factory : PlaceholderFactory<ProjectileView, ProjectileController>
    {
    }
}
