using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using Zenject;
using UniRx;

public class LevelManagerController: IInitializable
{
    private GameOver _gameOverPanel;
    private LevelWon _levelWonPanel;
    LevelManagerView _levelManagerView;
    HealthBarController _healthBar;

    public LevelManagerController (LevelManagerView levelManagerView, HealthBarController healthBar){
        _levelManagerView = levelManagerView;
        _healthBar = healthBar;
        _gameOverPanel = _levelManagerView._gameOverPanel;
        _levelWonPanel = _levelManagerView._levelWonPanel;
    }

    public void Initialize(){
        Time.timeScale = 1.0f;
    }    

    public void RestartLevel(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GameOver(){
        Time.timeScale = 0;
        _gameOverPanel.gameObject.SetActive(true);
        _gameOverPanel._myButton.OnClickAsObservable()
            .Subscribe(_ => RestartLevel());
    }

    public void LevelWon(){
        Time.timeScale = 0;
        _levelWonPanel.gameObject.SetActive(true);
        _levelWonPanel._restartButton.OnClickAsObservable()
            .Subscribe(_ => RestartLevel());
        _levelWonPanel._nextLevelButton.OnClickAsObservable()
            .Subscribe(_ => SceneManager.LoadScene("Level2"));
    }

    public void DamagePlayer(){

        _healthBar.DamageHealth();
        if (_healthBar._lives.Count == 0 ){
            GameOver();            
        }
    }

}
