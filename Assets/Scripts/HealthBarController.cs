using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarController : IPooledObject
{
    public List<GameObject> _lives = new List<GameObject>();

    HealthBarView _healthBarView;

    public HealthBarController(HealthBarView healthBarView){
        _healthBarView = healthBarView;
        OnObjectSpawn();
    }

    public void OnObjectSpawn()
    {
        _lives = _healthBarView._lives;
    }

    public void DamageHealth(){
        _lives[0].SetActive(false);
        _lives.RemoveAt(0);
    }

}
