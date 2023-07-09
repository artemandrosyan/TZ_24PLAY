using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackEffect : MonoBehaviour
{
    [SerializeField]
    private CubesStack _cubesStack;

    [SerializeField]
    private ParticleSystem _stackingEffect;

    private void Awake()
    {
        AddListeners();
    }

    private void PlayStackingEffect()
    {
        _stackingEffect.Play();
    }

    private void OnDestroy()
    {
        RemoveListeners();
    }

    private void AddListeners()
    {
        _cubesStack.StackNewCube += PlayStackingEffect;
    }

    private void RemoveListeners()
    {
        _cubesStack.StackNewCube -= PlayStackingEffect;
    }


}
