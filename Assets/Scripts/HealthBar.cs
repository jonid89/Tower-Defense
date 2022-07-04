using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] List<GameObject> lives = new List<GameObject>();

    [SerializeField] LevelManager levelManager;


    /*#region Singleton
    public static HealthBar Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }

    #endregion*/


    public void DamagePlayer(){
        lives[0].SetActive(false);
        lives.RemoveAt(0);
        if (lives.Count == 0 ){
            levelManager.GameOver();            
        }
    }


}
