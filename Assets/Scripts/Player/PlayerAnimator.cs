using Sirenix.OdinInspector;
using System;
using UnityEngine;
using Zenject;

public class PlayerAnimator : MonoBehaviour
{
    private Animator _animator;
    [SerializeField]
    private CubesStack _cubesStack;
    private static readonly int IdleHash = Animator.StringToHash("Idle");
    private static readonly int JumpHash = Animator.StringToHash("Jump");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        PlayIdle();
        AddListeners();
    }

    public void PlayIdle()
    {
        _animator.SetTrigger(IdleHash);
    }
    [Button]
    public void PlayJump()
    {
        _animator.SetTrigger(JumpHash);
    }

    private void OnDestroy()
    {
        RemoveListeners();
    }

    private void AddListeners()
    {
        _cubesStack.StackNewCube += PlayJump;
    }

    private void RemoveListeners()
    {
        _cubesStack.StackNewCube -= PlayJump;
    }
}
