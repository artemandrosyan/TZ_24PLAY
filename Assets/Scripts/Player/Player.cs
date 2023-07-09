using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Action<bool> ObstacleWasRaised;
    public Action ObstacleWasAttached;

    #if UNITY_EDITOR
    private const string MOOUSE_X = "Mouse X";
    private const float MOVE_BY_MOUSE_SPEED = 12f;
    #else
    private const float MOVE_BY_TOUCH_SPEED = 12f;
    private const float MOVE_BY_TOUCH_SENSIVITY = 0.5f;
    #endif

    private const float BOUND_OF_MOVE = 2f;
    [SerializeField]
    private LoseDetector _loseDetector;
    [SerializeField]
    private CubesStack _cubesStack;

    float xAxisValue;
    float newXPos;

    private bool _canMove;

    private void Awake()
    {
        AddListeners();
        InitCameraTarget();
        SetMoveState(false);
    }

    private void InitCameraTarget()
    {
        Camera.main.gameObject.GetComponent<CameraFollow>().Follow(gameObject);
    }

    private void AddListeners()
    {
        _loseDetector.DetectedObstacle += PlayerAttachedObstacle;
        _cubesStack.PlayerAttachedSurface += PlayerAttachedObstacle;
    }

    private void PlayerAttachedObstacle()
    {
        ObstacleWasAttached?.Invoke();
        SetMoveState(false);
        _cubesStack.SetStackingState(false);
    }

    private void Update()
    {
        if (!_canMove)
            return;

#if UNITY_EDITOR

        MoveByMouse();
#else
        MoveByTouch();
#endif
    }
#if UNITY_EDITOR
    private void MoveByMouse()
    {
        transform.Translate(Vector3.forward * MOVE_BY_MOUSE_SPEED * Time.deltaTime);
        if (Input.GetMouseButton(0))
        {
            xAxisValue = Input.GetAxis(MOOUSE_X);
            newXPos = transform.position.x + xAxisValue * MOVE_BY_MOUSE_SPEED * Time.fixedDeltaTime;
            newXPos = Mathf.Clamp(newXPos, -BOUND_OF_MOVE, BOUND_OF_MOVE);
            transform.position = new Vector3(newXPos, transform.position.y, transform.position.z);
        }
    }
#else
    private void MoveByTouch()
    {
        transform.Translate(Vector3.forward * MOVE_BY_TOUCH_SPEED * Time.deltaTime);

        if (Input.touchCount > 0)
        {
            var offset = transform.position;
            var t = Input.GetTouch(0);
            var delta = t.deltaPosition;

            offset.x += delta.x * MOVE_BY_TOUCH_SENSIVITY * ((float)Screen.width / Screen.height) * Time.deltaTime;
            offset.x = Mathf.Clamp(offset.x, -BOUND_OF_MOVE, BOUND_OF_MOVE);
            transform.position = offset;

        }

    }
#endif

    public void SetMoveState(bool state)
    {
        _canMove = state;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(GameConstants.OBSTACLE_ZONE))
        {
            ObstacleWasRaised?.Invoke(false);
        }
    }

    private void OnDestroy()
    {
        RemoveListeners();
    }

    private void RemoveListeners()
    {
        _loseDetector.DetectedObstacle -= PlayerAttachedObstacle;
        _cubesStack.PlayerAttachedSurface -= PlayerAttachedObstacle;
    }
}
