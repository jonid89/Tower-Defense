using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using Zenject;
using UniRx;
using UnityEngine.UI;

public class LevelManagerController: IInitializable
{
    private GameOver _gameOverPanel;
    private LevelWon _levelWonPanel;
    LevelManagerView _levelManagerView;
    HealthBarController _healthBar;
    EnemySpawnerController _enemySpawnerController;
    private Text _enemiesCountDisplayer;
    private int enemiesLeft;

    public LevelManagerController (LevelManagerView levelManagerView, HealthBarController healthBar, EnemySpawnerController enemySpawnerController){
        _levelManagerView = levelManagerView;
        _healthBar = healthBar;
        _enemySpawnerController = enemySpawnerController;
        _gameOverPanel = _levelManagerView._gameOverPanel;
        _levelWonPanel = _levelManagerView._levelWonPanel;
        _enemiesCountDisplayer = _levelManagerView._enemiesCountDisplayer;
    }

    public void Initialize(){
        Time.timeScale = 1.0f;
        enemiesLeft = _enemySpawnerController.GetEnemiesAmount;
        DisplayEnemiesCount();
    }    

    public void RestartLevel(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void RestartGame(){
        SceneManager.LoadScene("Level1");
    }

    public void GameOver(){
        Time.timeScale = 0;
        _gameOverPanel.gameObject.SetActive(true);
        _gameOverPanel._myButton.OnClickAsObservable()
            .Subscribe(_ => RestartLevel());
    }

    public void LevelWon(){
        Time.timeScale = 0;
        if(SceneManager.GetActiveScene().name != "Level2"){
            _levelWonPanel.gameObject.SetActive(true);
            _levelWonPanel._restartLevelButton.OnClickAsObservable()
                .Subscribe(_ => RestartLevel());
            _levelWonPanel._nextLevelButton.OnClickAsObservable()
                .Subscribe(_ => SceneManager.LoadScene("Level2"));
        }else{
            _levelWonPanel._nextLevelButton.gameObject.SetActive(false);
            _levelWonPanel._restartLevelButton.gameObject.transform.localPosition = new Vector3(0,-65,0);
            _levelWonPanel._restartGameButton.gameObject.SetActive(true);
            _levelWonPanel.gameObject.SetActive(true);
            _levelWonPanel._restartLevelButton.OnClickAsObservable()
                .Subscribe(_ => RestartLevel());
            _levelWonPanel._restartGameButton.OnClickAsObservable()
                .Subscribe(_ => RestartGame());
        }
    }

    public void DamagePlayer(){
        _healthBar.DamageHealth();
        if (_healthBar._lives.Count == 0 ){
            GameOver();            
        }
    }

    public void EnemyDestroyed(){
        enemiesLeft -- ;
        DisplayEnemiesCount();
        if (enemiesLeft <= 0 && _healthBar._lives.Count > 0){
            LevelWon();
        }
    }

    public void DisplayEnemiesCount(){
        _enemiesCountDisplayer.text = $"Enemies: {enemiesLeft}";
    }

}
