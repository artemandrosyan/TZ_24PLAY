using DG.Tweening;
using System;
using UnityEngine;

public class UsefulBlock : MonoBehaviour, IPoolElement
{
    private const float DELAY_FOR_SET_GRAVITY = 0.5f;
    private const float DELAY_FOR_DEACTIVATE = 10f;
    public Action<GameObject> ObstacleAttached;
    public Action<GameObject> CubeStacked;

    public bool IsStacked { get => isStacked; set => isStacked = value; }
    private bool isStacked = false;

    private Tween _setGravity;
    private Tween _deactivate;

    private Rigidbody _blockRB;

    private void Awake()
    {
        _blockRB = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(GameConstants.OBSTACLE_BOX))
        {
            transform.parent = null;
            ObstacleAttached?.Invoke(gameObject);
            isStacked = false;
            _setGravity = DOVirtual.DelayedCall(
                DELAY_FOR_SET_GRAVITY,
                () => {
                    GetComponent<BoxCollider>().isTrigger = false;
                    GetComponent<Rigidbody>().useGravity = true;
                    GetComponent<Rigidbody>().isKinematic = false;
                }
                );
            
            _deactivate = DOVirtual.DelayedCall(
                DELAY_FOR_DEACTIVATE,
                () => Deactivate()
                );
        }
    }

    private void OnDisable()
    {
        ResetBlock();
    }
    public void ResetBlock()
    {
        _setGravity.Kill();
        _deactivate.Kill();
        _blockRB.useGravity = false;
        _blockRB.isKinematic = true;
    }

    public void Deactivate()
    {
        _setGravity.Kill();
        _deactivate.Kill();
        gameObject?.SetActive(false);
    }

}
