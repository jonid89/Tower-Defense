using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using Zenject;
using UniRx;

public class LevelManager : MonoBehaviour
{
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject levelWonPanel;

    HealthBar _healthBar;

    [Inject]
    public void Construct (HealthBar healthBar){
        _healthBar = healthBar;
    }

    public void RestartLevel(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }

    public void GameOver(){
        Time.timeScale = 0;
        gameOverPanel.SetActive(true);
    }

    public void LevelWon(){
        Time.timeScale = 0;
        levelWonPanel.SetActive(true);
    }

    public void DamagePlayer(){
        _healthBar.DamageHealth();
    }

}
