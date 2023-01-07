using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarController : IPooledObject
{
    public List<GameObject> lives = new List<GameObject>();

    LevelManagerController _levelManager;

    HealthBarView _healthBarView;


    public HealthBarController(HealthBarView healthBarView){
        _healthBarView = healthBarView;
        OnObjectSpawn();
    }

    public void OnObjectSpawn()
    {
        lives = _healthBarView.lives;
    }

    public void DamageHealth(){
        lives[0].SetActive(false);
        lives.RemoveAt(0);
    }

}
