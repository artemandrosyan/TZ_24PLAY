using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisibleInformer : MonoBehaviour
{
    public Action BecameInvisible;
    private Tween _inform;
    private void OnBecameInvisible()
    {
        _inform = DOVirtual.DelayedCall(
            1f, 
            () => BecameInvisible?.Invoke()
            );
    }

    private void OnDestroy()
    {
        _inform.Kill();
    }
}
