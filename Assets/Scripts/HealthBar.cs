using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] List<GameObject> lives = new List<GameObject>();

    [SerializeField] LevelManager levelManager;


    public void DamageHealth(){
        lives[0].SetActive(false);
        lives.RemoveAt(0);
        if (lives.Count == 0 ){
            levelManager.GameOver();            
        }
    }


}
