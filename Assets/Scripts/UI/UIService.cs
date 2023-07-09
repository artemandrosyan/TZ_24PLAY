using DG.Tweening;
using Sirenix.OdinInspector;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

public class UIService : MonoBehaviour
{
    private const float DELAY_FOR_HAND_MOVE_TIME = 2f;
    private const float END_VALUE_FOR_HAND_MOVE_TIME = -200f;
    private const float TIME_FOR_HAND_MOVE_TIME = 1f;
    private const float DELAY_FOR_HIDE_START_PANEL = 0.5f;
    private const string SCENE_NAME = "GameScene";
    [FoldoutGroup("Start Panel")]
    [SerializeField]
    private GameObject _startPanel;
    [FoldoutGroup("Start Panel")]
    [SerializeField]
    private TMP_Text _holdToMoveText;
    [FoldoutGroup("Start Panel")]
    [SerializeField]
    private Transform _hand;

    [FoldoutGroup("Lose Panel")]
    [SerializeField]
    private GameObject _losePanel;
    [FoldoutGroup("Lose Panel")]
    [SerializeField]
    private TMP_Text _failText;
    [FoldoutGroup("Lose Panel")]
    [SerializeField]
    private Button _restartButton;

    private Player _player;

    private Action<bool> TutorialShowed;

    [Inject]
    private void Construct(Player player)
    {
        _player = player;
        AddListeners();
    }

    private void GameOver()
    {
        _losePanel.SetActive(true);
    }

    private void Awake()
    {
        Time.timeScale = 1f;
        MoveHand();
    }

    private void OnDestroy()
    {
        RemoveListeners();
    }

    private void AddListeners()
    {
        _restartButton.onClick.AddListener(RestartGame);
        _player.ObstacleWasAttached += GameOver;
        TutorialShowed += _player.SetMoveState;
    }

    private void RemoveListeners()
    {
        _player.ObstacleWasAttached -= GameOver;
        TutorialShowed -= _player.SetMoveState;
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(SCENE_NAME);
    }

    private void MoveHand()
    {
        DOVirtual.DelayedCall(DELAY_FOR_HAND_MOVE_TIME, () =>
        {
            _hand.DOLocalMoveX(END_VALUE_FOR_HAND_MOVE_TIME, TIME_FOR_HAND_MOVE_TIME).SetEase(Ease.InSine).OnComplete(() =>
            {
                DOVirtual.DelayedCall(DELAY_FOR_HIDE_START_PANEL, () => {
                    _startPanel.SetActive(false);
                    TutorialShowed?.Invoke(true);
                });

            });
        });
        
    }

}
