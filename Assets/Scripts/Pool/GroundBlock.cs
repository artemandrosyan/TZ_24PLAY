using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GroundBlock : MonoBehaviour, IPoolElement
{
    private const float USEFUL_BLOCK_DISTANCE = 2f;
    private const float OBSTACLE_BLOCK_DISTANCE = 1f;
    private const float BLOCK_START_X_POSITION = -2f;
    [SerializeField]
    private Transform _usefulBlocksParent;
    [SerializeField]
    private Transform _obstacleBlocksParent;
    private UsefulBlockPool _usefulBlockPoolContainer;
    private ObstacleBlockPool _obstacleBlockPoolContainer;
    private BlockTemplates _blockTemplates;

    [SerializeField]
    private VisibleInformer _visibleInformer;

    private List<UsefulBlock> _currentUsefulBlocks = new();
    private List<ObstacleBlock> _currentObstacleBlocks = new();

    public void Init(UsefulBlockPool usefulBlockPool, ObstacleBlockPool obstacleBlockPool, BlockTemplates blockTemplates)
    {
        _usefulBlockPoolContainer = usefulBlockPool;
        _obstacleBlockPoolContainer = obstacleBlockPool;
        _blockTemplates = blockTemplates;

        InitUsefulBlockGroup();
        InitObstacleGroup();

        AddListeners();
    }

    private void InitUsefulBlockGroup()
    {
        UsefulBlockGroup usefulBlockGroup = _blockTemplates.GetRandomUsefulBlockTemplate();
        int side = usefulBlockGroup.Size;

        for (int i = 0; i < side; i++)
        {
            for (int j = 0; j < side; j++)
            {
                if (!usefulBlockGroup.UsefulBlockField[i, j]) 
                    continue;


                UsefulBlock usefulBlock = _usefulBlockPoolContainer._pool.GetFreeElement();
                usefulBlock.gameObject.transform.SetParent(_usefulBlocksParent);
                usefulBlock.gameObject.transform.localPosition = new Vector3(BLOCK_START_X_POSITION + j * USEFUL_BLOCK_DISTANCE, 0f, (side - 1 - i)* USEFUL_BLOCK_DISTANCE);

                _currentUsefulBlocks.Add(usefulBlock);
                usefulBlock.ObstacleAttached += RemoveUsefulBlockFromList;
                usefulBlock.CubeStacked += RemoveUsefulBlockFromList;
            }
        }
    }



    private void InitObstacleGroup()
    {
        ObstacleGroup obstacleGroup = _blockTemplates.GetRandomObstacleTemplate();
        int side = obstacleGroup.Size;

        for (int i = 0; i < side; i++)
        {
            for (int j = 0; j < side; j++)
            {
                if (!obstacleGroup.ObstaclesBlockField[i, j])
                    continue;


                ObstacleBlock obstacleBlock = _obstacleBlockPoolContainer._pool.GetFreeElement();
                obstacleBlock.gameObject.transform.SetParent(_obstacleBlocksParent);
                obstacleBlock.gameObject.transform.localPosition = new Vector3(BLOCK_START_X_POSITION + j * OBSTACLE_BLOCK_DISTANCE, (side-1 - i) * 1f, 0f);

                _currentObstacleBlocks.Add(obstacleBlock);
            }
        }
    }
    private void RemoveUsefulBlockFromList(GameObject usefulCube)
    {
        var _usefulCube = usefulCube.GetComponent<UsefulBlock>();
        _usefulCube.ObstacleAttached -= RemoveUsefulBlockFromList;
        _usefulCube.CubeStacked -= RemoveUsefulBlockFromList;
        _currentUsefulBlocks.Remove(_usefulCube);
    }


    private void DeactivateAll()
    {
        if (!gameObject.activeInHierarchy)
            return;

        foreach (UsefulBlock _usefulBlock in _currentUsefulBlocks)
        {
            if (!_usefulBlock.IsStacked)
                _usefulBlock.Deactivate();
        }
        foreach (ObstacleBlock _obstacleBlock in _currentObstacleBlocks)
        {
                _obstacleBlock.Deactivate();
        }

        _currentObstacleBlocks.Clear();
        _currentUsefulBlocks.Clear();

        Deactivate();
    }

    public void Deactivate()
    {
        RemoveListeners();
        gameObject.SetActive(false);
    }


    private void AddListeners()
    {
        _visibleInformer.BecameInvisible += DeactivateAll;
    }

    private void RemoveListeners()
    {
        _visibleInformer.BecameInvisible -= DeactivateAll;
    }

    private void OnDestroy()
    {
        RemoveListeners();
        _currentObstacleBlocks.Clear();
        _currentUsefulBlocks.Clear();
    }
}
