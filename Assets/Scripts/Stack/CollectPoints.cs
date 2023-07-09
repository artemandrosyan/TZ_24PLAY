using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CollectPoints : MonoBehaviour
{
    [SerializeField]
    private CubesStack _cubesStack;
    private PointTextPool _pointTextPool;
    private Vector3 _startingOffset = new Vector3(0f, 0f, 5f);

    [Inject]
    private void Construct(PointTextPool pointTextPool)
    {
        _pointTextPool = pointTextPool;
    }

    private void InstantiatePointText()
    {
        PointText pointText = _pointTextPool._pool.GetFreeElement();
        pointText.transform.position = transform.position + _startingOffset;
        pointText.Move(transform.position);
    }
    private void Awake()
    {
        AddListeners();
    }

    private void OnDestroy()
    {
        RemoveListeners();
    }

    private void AddListeners()
    {
        _cubesStack.StackNewCube += InstantiatePointText;
    }

    private void RemoveListeners()
    {
        _cubesStack.StackNewCube -= InstantiatePointText;
    }

}
