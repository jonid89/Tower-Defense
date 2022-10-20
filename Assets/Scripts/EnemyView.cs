using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UniRx;
using UniRx.Triggers;
using Zenject;
using DG.Tweening;

public class EnemyView : MonoBehaviour, IPooledObject
{

    [SerializeField] private float _timeToFinishPath;
    public EnemyController _enemyController;
    public EnemyPath _enemyPath;
    private List<Vector3> _waypointsPositions = new List<Vector3>();
    private Tweener _path;
    private Animator _animator;
    private Vector2 _startPoint = new Vector2();
    private Vector2 _finalPoint = new Vector2();
    private Vector2 _direction = new Vector2();

    public void OnObjectSpawn()
    {   
        _animator = this.GetComponent<Animator>();
        _startPoint = this.gameObject.transform.position;

        _waypointsPositions = _enemyPath.getWaypoints();
        float speed = Random.Range(_timeToFinishPath-2,_timeToFinishPath+2);
        _path = this.gameObject.transform.DOPath(_waypointsPositions.ToArray(), _timeToFinishPath, PathType.Linear, PathMode.Full3D)
            .SetEase(Ease.Linear)
            .OnWaypointChange(WalkAnimation)
            .OnStepComplete( () => _enemyController.EndReached());
    }

    public void WalkAnimation(int waypointIndex){
        _finalPoint = _waypointsPositions[waypointIndex];
        _direction = (_finalPoint - _startPoint);
        _direction.Normalize();
        

        if(_direction == Vector2.up){
            _animator.Play("GreySpiderWalkUp");
        }
        if(_direction == Vector2.down){
            _animator.Play("GreySpiderWalkDown");
        }
        if(_direction == Vector2.left){
            _animator.Play("GreySpiderWalkLeft");
        }
        if(_direction == Vector2.right){
            _animator.Play("GreySpiderWalkRight");
        }

        _startPoint = _finalPoint;
        
    }


    public class Pool : MemoryPool<EnemyView>
    {
        
    }

}
