using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundBlockPool : MonoBehaviour
{
    [SerializeField]
    private int count;
    [SerializeField]
    private GroundBlock _groundBlockPrefab;

    public Pool<GroundBlock> _pool;

    private void Awake()
    {
        _pool = new Pool<GroundBlock>(_groundBlockPrefab, count, transform);
    }

    private void OnDestroy()
    {
        _pool.ClearPool();
    }
}
