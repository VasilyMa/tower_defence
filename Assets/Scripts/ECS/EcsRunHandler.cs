using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Statement;
using System.Collections.Generic;

public class EcsRunHandler
{
    EcsWorld _world;
    EcsSystems _initSystems;
    EcsSystems _commonSystems;

    List<EcsSystems> _systems;

    public EcsRunHandler(BattleState state)
    {
        _world = new EcsWorld();

        _initSystems = new EcsSystems(_world, state);
        _commonSystems = new EcsSystems(_world, state);

#if UNITY_EDITOR
        _commonSystems.Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem());
#endif 

        _systems = new List<EcsSystems>()
        {
            _initSystems,
            _commonSystems
        };
    }

    public void Init()
    {
        foreach (var system in _systems)
        {
            system.Inject();
            system.Init();
        }
    }

    public EcsPackedEntity PackEntity(int entity)
    {
        return _world.PackEntity(entity);
    }

    public bool Unpack(EcsPackedEntity pack, out int entity)
    {
        return pack.Unpack(_world, out entity);
    }

    public void Run()
    {
        foreach (var system in _systems)
        {
            system.Run();
        }
    } 

    public void FixedRun()
    { 
        foreach (var system in _systems)
        {
            system.Run();
        }
    }

    public void AfterRun()
    { 
        foreach (var system in _systems)
        {
            system.Run();
        }
    }

    public void Dispose()
    {
        foreach (var system in _systems)
        {
            system.Destroy();
        } 
    }
}
