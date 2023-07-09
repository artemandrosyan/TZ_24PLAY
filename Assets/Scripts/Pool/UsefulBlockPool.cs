using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsefulBlockPool: MonoBehaviour
{
    [SerializeField]
    private int count;
    [SerializeField]
    private UsefulBlock _usefulBlockPrefab;

    public Pool<UsefulBlock> _pool;

    private void Awake()
    {
        _pool = new Pool<UsefulBlock>(_usefulBlockPrefab, count, transform);
    }

    private void OnDestroy()
    {
        _pool.ClearPool();
    }
}
