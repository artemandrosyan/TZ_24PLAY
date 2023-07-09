using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BootstrapInstaller : MonoInstaller
{
    [SerializeField]
    private GameObject _levelBuilderPrefab;
    [SerializeField]
    private GameObject _playerPrefab;
    [SerializeField]
    private GameObject _blockTemplatesPrefab;
    [SerializeField]
    private GameObject _usefulBlockPrefab;
    [SerializeField]
    private GameObject _obstacleBlockPrefab;
    [SerializeField]
    private GameObject _groundBlockPrefab;
    [SerializeField]
    private GameObject _pointTextPrefab;
    [SerializeField]
    private GameObject _UIPrefab;

    public override void InstallBindings()
    {
        BindPools();
        BindBlockTemplates();
        BindPlayer();
        BindLevelBuilder();
        BindUI();
    }

    private void BindUI()
    {
        UIService _UI = Container.InstantiatePrefabForComponent<UIService>(_UIPrefab);
        Container.Bind<UIService>()
        .FromInstance(_UI)
        .AsSingle();
    }

    private void BindLevelBuilder()
    {
        LevelBuilder _levelBuilder = Container.InstantiatePrefabForComponent<LevelBuilder>(_levelBuilderPrefab);
        Container.Bind<LevelBuilder>()
        .FromInstance(_levelBuilder)
        .AsSingle();
    }

    private void BindPlayer()
    {
        Player _player = Container.InstantiatePrefabForComponent<Player>(_playerPrefab);
        Container.Bind<Player>()
        .FromInstance(_player)
        .AsSingle();
    }

    private void BindBlockTemplates()
    {
        BlockTemplates _blockTemplates = Container.InstantiatePrefabForComponent<BlockTemplates>(_blockTemplatesPrefab);
        Container.Bind<BlockTemplates>()
        .FromInstance(_blockTemplates)
        .AsSingle();
    }

    private void BindPools()
    {
        UsefulBlockPool _usefulBlockPool = Container.InstantiatePrefabForComponent<UsefulBlockPool>(_usefulBlockPrefab);
        Container.Bind<UsefulBlockPool>()
        .FromInstance(_usefulBlockPool)
        .AsSingle();

        ObstacleBlockPool _obstacleBlockPool = Container.InstantiatePrefabForComponent<ObstacleBlockPool>(_obstacleBlockPrefab);
        Container.Bind<ObstacleBlockPool>()
        .FromInstance(_obstacleBlockPool)
        .AsSingle();

        GroundBlockPool _groundBlockPool = Container.InstantiatePrefabForComponent<GroundBlockPool>(_groundBlockPrefab);
        Container.Bind<GroundBlockPool>()
        .FromInstance(_groundBlockPool)
        .AsSingle();

        PointTextPool _pointTextPool = Container.InstantiatePrefabForComponent<PointTextPool>(_pointTextPrefab);
        Container.Bind<PointTextPool>()
        .FromInstance(_pointTextPool)
        .AsSingle();
    }
}
