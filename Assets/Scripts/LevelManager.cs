using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class LevelManager : MonoBehaviour
{
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject levelWonPanel;

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

}
