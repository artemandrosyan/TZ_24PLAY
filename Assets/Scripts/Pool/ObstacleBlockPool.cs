using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBlockPool : MonoBehaviour
{
    [SerializeField]
    private int count;
    [SerializeField]
    private ObstacleBlock _obstacleBlockPrefab;

    public Pool<ObstacleBlock> _pool;

    private void Awake()
    {
        _pool = new Pool<ObstacleBlock>(_obstacleBlockPrefab, count, transform);
    }

    private void OnDestroy()
    {
        _pool.ClearPool();
    }
}
