using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointText : MonoBehaviour, IPoolElement
{
    private const float TIME_FOR_MOVE = 0.5f;
    private const float TIME_FOR_SCALE = 0.5f;
    private const int SCALE_KOEFF = 2;
    private const float DELAY_FOR_DEACTIVATE = 3f;
    private Vector3 _offsetForMove = new Vector3(3f, 5f, 10f);

    private Tween _movePointtext;


    public void Move(Vector3 currentPos)
    {
        transform.DOMoveX(currentPos.x - _offsetForMove.x, TIME_FOR_MOVE).SetEase(Ease.InQuad);
        transform.DOMoveY(currentPos.y + _offsetForMove.y, TIME_FOR_MOVE).SetEase(Ease.InQuad);
        transform.DOMoveZ(currentPos.z + _offsetForMove.z, TIME_FOR_MOVE).SetEase(Ease.OutQuad);

        Vector3 startScale = transform.localScale;
        transform.DOScale(startScale * SCALE_KOEFF, TIME_FOR_SCALE);

        _movePointtext = DOVirtual.DelayedCall(
            DELAY_FOR_DEACTIVATE,
            () =>
            {
                transform.localScale = startScale;
                Deactivate();
            });
    }

    public void Deactivate()
    {
        _movePointtext.Kill();
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        _movePointtext.Kill();
    }
}
