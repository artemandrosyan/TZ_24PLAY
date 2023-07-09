using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubesStack : MonoBehaviour
{
    private const float DELAY_BEFORE_REBUILD_STACK = 0.2f;
    private const float MOVE_CUBE_DURATION = 0.1f;
    private const float DELAY_KOEFF = 0.05f;
    private const float SCALE_ANUMATION_VALUE = 0.8f;
    private const float SCALE_ANIMATION_TIME = 0.1f;
    private const float PLAYER_Y_OFFSET = 0.5f;
    private const float PLAYER_MOVE_ANIMATIOONTIME = 0.1f;
    public Action StackNewCube;
    public Action ObstacleAttached;
    public Action PlayerAttachedSurface;

    [SerializeField]
    private Transform _player;
    [SerializeField]
    private GameObject _defaultCube;
    [SerializeField]
    private List<GameObject> _cubesList = new();

    private bool _isTriggered = false;
    private bool _canStacking;
    private Tween _rebuildStack;

    private void Awake()
    {
        SetStackingState(true);
        InitDefaultCube();
    }

    private void InitDefaultCube()
    {
        var _defaultUsefulCube = _defaultCube.GetComponent<UsefulBlock>();
        _defaultUsefulCube.IsStacked = true;
        _defaultUsefulCube.ObstacleAttached = RemoveCube;
        _cubesList.Add(_defaultCube);
    }

    private void AddCube(GameObject newCube)
    {
        if (_isTriggered || _cubesList.Contains(newCube) || !_canStacking)
            return;

        _isTriggered = true;
        StackNewCube?.Invoke();

        RebuildStackAfterStacking();

        _cubesList.Add(newCube);

        var _usefulBlock = newCube.gameObject.GetComponent<UsefulBlock>();
        _usefulBlock.IsStacked = true;
        _usefulBlock.CubeStacked?.Invoke(newCube);
        _usefulBlock.ObstacleAttached = RemoveCube;

        _isTriggered = false;
    }

    private void RebuildStackAfterStacking()
    {
        for (int i = 0; i < _cubesList.Count; i++)
        {
            _cubesList[i].transform.position = _cubesList[i].transform.position.Up();
        }

        _player.position = _player.position.Up();
    }

    internal void SetStackingState(bool state)
    {
        _canStacking = state;
        Time.timeScale = 0f;
    }

    private void RemoveCube(GameObject cubeForRemove)
    {
        if (!_cubesList.Contains(cubeForRemove) || !_canStacking) 
            return;

        DOVirtual.DelayedCall(0.1f, ()=> _cubesList.Remove(cubeForRemove));
        ObstacleAttached?.Invoke();

        _rebuildStack.Kill();

        _rebuildStack = DOVirtual.DelayedCall(
            DELAY_BEFORE_REBUILD_STACK,
            () =>
            {
                RebuildStackAfterObstacles();
            });
    }

    private void RebuildStackAfterObstacles()
    {
        for (int i = 0; i < _cubesList.Count; i++)
        {
            _cubesList[i].transform.DOLocalMoveY(i, MOVE_CUBE_DURATION).SetEase(Ease.InSine).SetDelay(DELAY_KOEFF * i);
            _cubesList[i].transform.DOScaleY(SCALE_ANUMATION_VALUE, SCALE_ANIMATION_TIME).SetLoops(2, LoopType.Yoyo).SetDelay(DELAY_KOEFF * i);
            if(i == _cubesList.Count-1)
                _player.DOLocalMoveY(i+ PLAYER_Y_OFFSET, PLAYER_MOVE_ANIMATIOONTIME).SetEase(Ease.InSine).SetDelay(DELAY_KOEFF * i);
        }
       
        if (_cubesList.Count == 0)
            PlayerAttachedSurface?.Invoke();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(GameConstants.USEFUL_BOX))
        {
            other.isTrigger = true;
            AddCube(other.gameObject);
            other.gameObject.transform.SetParent(transform);
            other.gameObject.transform.localPosition = Vector3.zero;
        }
    }
}
