using UnityEngine;
using Zenject;
using DG.Tweening;

public class LevelBuilder : MonoBehaviour
{
    private const int START_COUNT_OF_GROUND_BLOCKS = 4;
    private const float BLOCK_SIZE = 30f;
    private const float GROUND_BLOCK_OFFSET = 9f;
    private const float MOVE_TIME_GROUND_BLOCK_Z = 0.5f;
    private const float MOVE_TIME_GROUND_BLOCK_X = 0.4f;
    private const float MOVE_TIME_GROUND_BLOCK_Y = 0.8f;
    private const float GROUND_BLOCK_STARTING_OFFSET_X = 5f;
    private const float GROUND_BLOCK_STARTING_OFFSET_Y = -20f;
    private const int GROUND_BLOCK_STARTING_OFFSET_Z_IN_BLOCKS = 4;

    private UsefulBlockPool _usefulBlockPoolContainer;
    private ObstacleBlockPool _obstacleBlockPoolContainer;
    private GroundBlockPool _groundBlockPoolContainer;
    private BlockTemplates _blockTemplates;
    private Player _player;
    private int _levelBlocksCount = 0;

    [Inject]
    private void Construct(UsefulBlockPool usefulBlockPool, ObstacleBlockPool obstacleBlockPool, GroundBlockPool groundBlockPool, BlockTemplates blockTemplates, Player player)
    {
        _usefulBlockPoolContainer = usefulBlockPool;
        _obstacleBlockPoolContainer = obstacleBlockPool;
        _groundBlockPoolContainer = groundBlockPool;
        _blockTemplates = blockTemplates;
        _player = player;
    }

    private void Awake()
    {
        InstantiateStartingBlocks();
        _player.ObstacleWasRaised = InstantiateNewLevelBlock;
    }

    private void InstantiateStartingBlocks()
    {
        for (int i = 0; i < START_COUNT_OF_GROUND_BLOCKS; i++)
        {
            InstantiateNewLevelBlock(true);
        }
    }

    private void InstantiateNewLevelBlock(bool isInit = false)
    {
        var newBlock = _groundBlockPoolContainer._pool.GetFreeElement();
        newBlock.Init(_usefulBlockPoolContainer, _obstacleBlockPoolContainer, _blockTemplates);

        if (isInit)
            newBlock.transform.position = new Vector3(0f, 0f, _levelBlocksCount * BLOCK_SIZE + GROUND_BLOCK_OFFSET);
        else
            MoveLevelBlockToPlace(newBlock);

        _levelBlocksCount++;
    }

    private void MoveLevelBlockToPlace(GroundBlock newBlock)
    {
        newBlock.transform.position = new Vector3(GROUND_BLOCK_STARTING_OFFSET_X, GROUND_BLOCK_STARTING_OFFSET_Y, (_levelBlocksCount - GROUND_BLOCK_STARTING_OFFSET_Z_IN_BLOCKS) * BLOCK_SIZE);
        newBlock.transform.DOMoveZ(_levelBlocksCount * BLOCK_SIZE + GROUND_BLOCK_OFFSET, MOVE_TIME_GROUND_BLOCK_Z);
        newBlock.transform.DOMoveX(0f, MOVE_TIME_GROUND_BLOCK_X);
        newBlock.transform.DOMoveY(0f, MOVE_TIME_GROUND_BLOCK_Y);
    }

}
