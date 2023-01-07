using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UniRx;
using UniRx.Triggers;
using Zenject;
using System.Threading.Tasks;
using DG.Tweening;

public class EnemyView : MonoBehaviour, IPooledObject
{

    [SerializeField] public int maxHealth;
    [SerializeField] private float _timeToFinishPath;
    public EnemyController _enemyController;
    public EnemyPath _enemyPath;
    private List<Vector3> _waypointsPositions = new List<Vector3>();
    public Tweener _path;
    private Vector2 _startPoint = new Vector2();
    private Vector2 _finalPoint = new Vector2();
    private Vector2 _direction = new Vector2();
    [SerializeField] private List<Sprite> _spritesWalkLeft;
    [SerializeField] private List<Sprite> _spritesWalkRight;
    [SerializeField] private List<Sprite> _spritesWalkUp;
    [SerializeField] private List<Sprite> _spritesWalkDown;
    private List<Sprite> _currentSprites;
    private int _spriteNumber = 0;
    [SerializeField] private float _animationSpeed = 0.3f;
    private float _counter;

    public void OnObjectSpawn()
    {   
        _startPoint = this.gameObject.transform.position;
        _currentSprites = _spritesWalkLeft;
        _counter = _animationSpeed;

        _waypointsPositions = _enemyPath.getWaypoints();
        float speed = Random.Range(_timeToFinishPath-2,_timeToFinishPath+2);
        
        _path = this.gameObject.transform.DOPath(_waypointsPositions.ToArray(), speed, PathType.Linear, PathMode.Full3D)
            .SetEase(Ease.Linear)
            .OnWaypointChange(SetSprites)
            .OnStepComplete( () => _enemyController.EndReached());
        
    }

    private void Update(){
        
       _counter -= Time.deltaTime;
        if(_counter <= 0){
            _spriteNumber++ ;
            if(_spriteNumber >= _currentSprites.Count ) _spriteNumber = 0;
            this.GetComponent<SpriteRenderer>().sprite = _currentSprites[_spriteNumber];
            _counter = _animationSpeed;
        } 
    }

    public void SetSprites(int waypointIndex){
        _finalPoint = _waypointsPositions[waypointIndex];
        _direction = (_finalPoint - _startPoint);
        _direction.Normalize();

        if(_direction == Vector2.up){
            _currentSprites = _spritesWalkUp;
        }
        if(_direction == Vector2.down){
            _currentSprites = _spritesWalkDown;
        }
        if(_direction == Vector2.left){
            _currentSprites = _spritesWalkLeft;
        }
        if(_direction == Vector2.right){
            _currentSprites = _spritesWalkRight;
        }

        _startPoint = _finalPoint;
        
    }

    public void EndEnemy(){
        gameObject.SetActive(false);
        _path.Restart();
        _path.Kill();
    }


    public class Pool : MemoryPool<EnemyView>
    {
        
    }

}
