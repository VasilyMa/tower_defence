using Client;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.ExtendedSystems;
using Statement;
using System.Collections.Generic;

public class EcsRunHandler
{
    EcsWorld _world;
    EcsSystems _initSystems;
    EcsSystems _commonSystems;
    EcsSystems _projectileSystems;
    EcsSystems _resolveSystems;
    EcsSystems _prepareSystems;
    EcsSystems _attackSystems;
    EcsSystems _addSystems;
    EcsSystems _damageSystems;
    EcsSystems _recycleSystems;
    EcsSystems _nullableSystems;

    List<EcsSystems> _systems;

    public EcsRunHandler(BattleState state)
    {
        _world = new EcsWorld();

        _initSystems = new EcsSystems(_world, state);
        _initSystems
            .Add(new InitInputSystem())
            .Add(new InitTowerSystem())
            .Add(new InitSpawnerSystem())
            .Add(new InitBattleFieldSystem())
            ;

        _commonSystems = new EcsSystems(_world, state);
        _commonSystems
            .Add(new RunSpawnerSystem())
            .Add(new RunInvokeEnemySystem())
            .Add(new RunSpawnEnemySystem())
            .Add(new RunMovementEnemySystem()) 
            .Add(new RunOffsetEnemySystem())

            .Add(new RunSpawnTowerSystem())

            .DelHere<SpawnEvent>()
            .DelHere<SpawnTowerEvent>()
            .DelHere<InvokeEvent, EnemyComponent>()
            ;

        _projectileSystems = new EcsSystems(_world, state);
        _projectileSystems
            .Add(new RunInvokeArrowSystem())
            .Add(new RunInvokeBalisticSystem())

            .Add(new RunMotionBalisticSystem())
            .Add(new RunMotionArrowSystem())

            .DelHere<InvokeEvent, ProjectileComponent>()
            ;

        _resolveSystems = new EcsSystems(_world, state);
        _resolveSystems
            .Add(new RunResolveDamageSystem())

            .Add(new RunResolveSystem())
            .DelHere<ResolveEvent, ProjectileComponent>()
            ;

        _prepareSystems = new EcsSystems(_world, state);
        _prepareSystems
            .Add(new RunTowerAimSystem())
            .Add(new RunTowerRotateSystem()) 
            ;

        _attackSystems = new EcsSystems(_world, state);
        _attackSystems
            .Add(new RunCooldownCompleteSystem())
            .Add(new RunCooldownUpdateSystem())
            .Add(new RunTowerAttackSystem())
            ;

        _addSystems = new EcsSystems(_world, state);
        _addSystems
            .Add(new RunAddComponentSystem<DurabilityComponent>())
            .Add(new RunAddComponentSystem<MovementComponent>())
            .Add(new RunAddComponentSystem<TargetComponent>())
            .Add(new RunAddComponentSystem<ArrowProjectileComponent>())
            .Add(new RunAddComponentSystem<BalisticProjectileComponent>())
            .Add(new RunAddComponentSystem<VelocityComponent>())
            .Add(new RunAddComponentSystem<DamageComponent>())
            .Add(new RunAddCompleteSystem())
            .DelHere<AddComponentEvent>()
            ;

        _damageSystems = new EcsSystems(_world, state);
        _damageSystems
            .Add(new RunTakeDamageSystem())
            .Add(new RunClearDamageSystem())
            .Add(new RunDieSystem())
            ;

        _recycleSystems = new EcsSystems(_world, state);
        _recycleSystems
            .Add(new RunRecycleSystem<BoundsComponent>())
            .Add(new RunRecycleSystem<ZigZagComponent>())
            .Add(new RunRecycleSystem<ArrowComponent>())
            .Add(new RunRecycleSystem<BalisticComponent>())
            .Add(new RunRecycleSystem<DestinationComponent>())
            .Add(new RunRecycleSystem<HealthComponent>())
            .Add(new RunRecycleSystem<ActiveState>())
            .Add(new RunRecycleSystem<DamageState>())
            .Add(new RunRecycleSystem<MoveState>())
            .Add(new RunRecycleCompleteSystem())
            .DelHere<RecycleEvent>()
            ;

        _nullableSystems = new EcsSystems(_world, state);
        _nullableSystems
            .Add(new RunNullableSystem<ActiveState, TransformComponent>());

#if UNITY_EDITOR
        _commonSystems.Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem());
#endif 

        _systems = new List<EcsSystems>()
        {
            _initSystems,
            _prepareSystems,
            _commonSystems,
            _projectileSystems,
            _resolveSystems,
            _attackSystems,
            _damageSystems,
            _addSystems,
            _recycleSystems,
            _nullableSystems,
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

    public void ThrowNewEvent<T>(T data) where T : struct
    {
        _world.GetPool<T>().Add(_world.NewEntity()) = data; 
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
    }

    public void AfterRun()
    {  
    }

    public void Dispose()
    {
        foreach (var system in _systems)
        {
            system.Destroy();
        } 
    }
}
