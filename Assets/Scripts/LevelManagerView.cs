using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using Zenject;
using UniRx;

public class LevelManagerView : MonoBehaviour
{
    [SerializeField] public GameOver _gameOverPanel;
    [SerializeField] public LevelWon _levelWonPanel;

}
