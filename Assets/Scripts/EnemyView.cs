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

    [SerializeField] public int _maxHealth;
    [SerializeField] public float _timeToFinishPath;
    public EnemyController _enemyController;
    public EnemyPath _enemyPath;
    private List<Vector3> _waypointsPositions = new List<Vector3>();
    [SerializeField] public SpriteRenderer _spriteRenderer;
    [SerializeField] public List<Sprite> _spritesWalkLeft;
    [SerializeField] public List<Sprite> _spritesWalkRight;
    [SerializeField] public List<Sprite> _spritesWalkUp;
    [SerializeField] public List<Sprite> _spritesWalkDown;
    public List<Sprite> _currentSprites;
    private int _spriteNumber = 0;
    [SerializeField] private float _animationSpeed = 0.3f;
    private float _counter;
    public Transform MyTransform{
        get { return this.gameObject.transform;}
    }

    public void OnObjectSpawn()
    {   
        _currentSprites = _spritesWalkLeft;
        _counter = _animationSpeed;
    }

    private void Update(){
        
       _counter -= Time.deltaTime;
        if(_counter <= 0){
            _spriteNumber++ ;
            if(_spriteNumber >= _currentSprites.Count ) _spriteNumber = 0;
            _spriteRenderer.sprite = _currentSprites[_spriteNumber];
            _counter = _animationSpeed;
        } 
    }

    public class Pool : MemoryPool<EnemyView>
    {   
    }

}
