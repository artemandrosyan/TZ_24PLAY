using DG.Tweening;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using UnityEngine;
using Zenject;

public class CameraFollow : MonoBehaviour
{
    private const float SHAKE_CAMERA_DURATION = 0.15f;
    private const float SHAKE_CAMERA_STRENGTH = 0.4f;
    private const int SHAKE_CAMERA_VIBRATO = 3;
    [SerializeField]
    private Transform _following;

    private Vector3 _startOffset;
    private bool _isSkaking;
    private float _smoothTime = 0.25f;
    private Vector3 _velocity = Vector3.zero;
    private CubesStack _cubeStack;

    private void Awake()
    {
        _startOffset = transform.position;

    }

    private void LateUpdate()
    {
        if (_following == null)
        {
            return;
        }
        if (!_isSkaking) 
        {

            Vector3 targetPosition = new Vector3(transform.position.x, transform.position.y, _following.position.z + _startOffset.z);
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _velocity, _smoothTime);
        }
        else
            ShakeCamera();
    }

    public void Follow(GameObject following)
    {
        _following = following.transform;

        _cubeStack = _following.GetComponentInChildren<CubesStack>();

        AddListeners();
    }

    private void ShakeCamera()
    {   if (_isSkaking) 
            return;
        
        _isSkaking = true;
        transform.DOShakePosition(SHAKE_CAMERA_DURATION, SHAKE_CAMERA_STRENGTH, SHAKE_CAMERA_VIBRATO).OnComplete(() =>
        _isSkaking = false);
    }

    private void OnDestroy()
    {
        RemoveListeners();
    }

    private void AddListeners()
    {
       _cubeStack.ObstacleAttached += ShakeCamera;
    }

    private void RemoveListeners()
    {
      _cubeStack.ObstacleAttached -= ShakeCamera;
    }
}