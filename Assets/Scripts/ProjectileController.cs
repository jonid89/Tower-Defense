using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Zenject;

public class ProjectileController : IPooledObject
{
    private EnemyState _enemy = null;
    private Vector3 _enemyPosition;
    ProjectileView _projectileView;
    private GameObject _towerParent;
    private TowerConfig _towerConfig;
    private float _projectilePathDuration;
    private int _projectileDamage;
    
    public ProjectileController(ProjectileView projectileView) {
        _projectileView = projectileView;
        OnObjectSpawn();
        fireAtEnemy();
    }

    public void OnObjectSpawn()
    {
        _towerParent = _projectileView.transform.parent.gameObject;
        _towerConfig = _towerParent.GetComponent<TowerView>().GetTowerConfig;
        _projectileDamage = _towerConfig._projectileDamage;
        _projectilePathDuration = _towerConfig._projectilePathDuration;
    }

    public void fireAtEnemy(){
        _enemy = _towerParent.GetComponent<TowerView>()._towerController.getTarget();
        if(_enemy == null) return;
        _enemyPosition = _enemy.transform.position;
        Vector3 projectilePos = _projectileView.transform.position;
        var startingPosition = _towerParent.transform.position;
        Debug.Log(_enemy);

        DOTween.To(x=>{ _projectileView.transform.position = Vector3.Lerp(startingPosition, _enemy.gameObject.transform.position, x);},
            0,1, _projectilePathDuration)
            .SetEase(Ease.Linear)
            .OnStepComplete( () => 
                {
                    _enemy._enemyController.GetDamage(_projectileDamage);
                    _projectileView.gameObject.SetActive(false);
                }
            );
    }

    public class Factory : PlaceholderFactory<ProjectileView, ProjectileController>
    {
    }
}
