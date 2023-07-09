using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockTemplates : MonoBehaviour
{
    private const string USEFUL_BLOCK_SO_PATH = "ScriptableObjects/UsefulBlocksSO";
    private const string OBSTACLE_SO_PATH = "ScriptableObjects/ObstaclesSO";
    [FoldoutGroup("Usefull blocks")]
    [SerializeField]
    private UsefulBlockGroup[] _usefulBlockTemplates;

    [FoldoutGroup("Obstacle blocks")]
    [SerializeField]
    private ObstacleGroup[] _obstacleGroupTemplates;

    private int _usefulBlockCount = 0;
    private int _obstacleBlockCount = 0;

    private void Awake()
    {
        _usefulBlockCount = _usefulBlockTemplates.Length;
        _obstacleBlockCount = _obstacleGroupTemplates.Length;
    }

    public UsefulBlockGroup GetRandomUsefulBlockTemplate()
    {
        int id = Random.Range(0, _usefulBlockCount);
        return _usefulBlockTemplates[id];
    }
    public ObstacleGroup GetRandomObstacleTemplate()
    {
        int id = Random.Range(0, _obstacleBlockCount);
        return _obstacleGroupTemplates[id];
    }

    [FoldoutGroup("Usefull blocks")]
    [Button]
    private void FillUsefullBlocksTemplates()=>
        _usefulBlockTemplates = Resources.LoadAll<UsefulBlockGroup>(USEFUL_BLOCK_SO_PATH);

    [FoldoutGroup("Obstacle blocks")]
    [Button]
    private void FillObstackleBlocksTemplates()=>
        _obstacleGroupTemplates = Resources.LoadAll<ObstacleGroup>(OBSTACLE_SO_PATH);

}
