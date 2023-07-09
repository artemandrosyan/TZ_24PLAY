using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointTextPool : MonoBehaviour
{
    [SerializeField]
    private int count;
    [SerializeField]
    private PointText _pointTextPrefab;

    public Pool<PointText> _pool;

    private void Awake()
    {
        _pool = new Pool<PointText>(_pointTextPrefab, count, transform);
    }

    private void OnDestroy()
    {
        _pool.ClearPool();
    }
}
