using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using Zenject;
using UniRx;

public class LevelManagerController
{
    private GameObject _gameOverPanel;
    private GameObject _levelWonPanel;
    LevelManagerView _levelManagerView;

    HealthBarController _healthBar;
    

    public LevelManagerController (LevelManagerView levelManagerView, HealthBarController healthBar){
        _levelManagerView = levelManagerView;
        _healthBar = healthBar;
        _gameOverPanel = _levelManagerView.gameOverPanel;
        _levelWonPanel = _levelManagerView.levelWonPanel;
    }

    public void RestartLevel(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }

    public void GameOver(){
        Time.timeScale = 0;
        _gameOverPanel.SetActive(true);
    }

    public void LevelWon(){
        Time.timeScale = 0;
        _levelWonPanel.SetActive(true);
    }

    public void DamagePlayer(){

        _healthBar.DamageHealth();
        if (_healthBar.lives.Count == 0 ){
            GameOver();            
        }
    }

}
