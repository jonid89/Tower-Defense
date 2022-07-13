using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class FinalWaypoint : MonoBehaviour
{
    HealthBar playerHealth;
    
    void Start()
    {
        playerHealth = HealthBar.Instance;
        
        this.OnTriggerEnter2DAsObservable()
            .Where(collision => collision.CompareTag("Enemy"))
            .Subscribe(_ =>
                    playerHealth.DamagePlayer()
            );
    }

    
}
