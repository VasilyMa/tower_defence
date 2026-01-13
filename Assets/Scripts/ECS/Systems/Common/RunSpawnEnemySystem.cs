using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Statement;
using System;
using UnityEngine;

namespace Client 
{
    sealed class RunSpawnEnemySystem : IEcsRunSystem 
    {
        readonly EcsWorldInject _world = default;
        readonly EcsSharedInject<BattleState> _state = default;
        readonly EcsFilterInject<Inc<SpawnEvent>> _filter = default;
        readonly EcsPoolInject<SpawnEvent> _spawnPool = default;
        readonly EcsPoolInject<EnemyComponent> _enemyPool = default;
        readonly EcsPoolInject<TransformComponent> _transformPool = default;
        readonly EcsPoolInject<AddComponentEvent> _addPool = default;
        readonly EcsPoolInject<DamageContainerComponent> _damageContainerPool = default;
        readonly EcsPoolInject<PoolComponent> _pool = default;
        readonly EcsPoolInject<BoundsComponent> _boundsPool = default;
        readonly EcsPoolInject<ZigZagComponent> _zigPool = default;
        readonly EcsPoolInject<BattlefieldComponent> _battlePool = default;

        public void Run (IEcsSystems systems) 
        {
            foreach (var entity in _filter.Value)
            {
                int enemyEntity = default;
                 
                ref var addComp = ref _addPool.Value.Add(entity);
                addComp.TargetEntity = ListPoolService<int>.Get();

                ref var spawnComp = ref _spawnPool.Value.Get(entity);

                for (global::System.Int32 i = 0; i < spawnComp.Count; i++)
                {
                    Vector3 spawn = spawnComp.SpawnPoint[UnityEngine.Random.Range(0, spawnComp.SpawnPoint.Count)];

                    if (!EntityPoolService.TryGet(_state.Value.Enemy.name, out enemyEntity))
                    {
                        string entityKey = Guid.NewGuid().ToString();

                        enemyEntity = _world.Value.NewEntity();

                        var enemyInstance = GameObject.Instantiate(_state.Value.Enemy, spawn, new Quaternion(0, 180, 0, 0));

                        ref var transformComp = ref _transformPool.Value.Add(enemyEntity);
                        transformComp.Transform = enemyInstance.transform;

                        enemyInstance.gameObject.name = entityKey;

                        _state.Value.AddEntity(entityKey, enemyEntity);

                        _pool.Value.Add(enemyEntity).PoolKeyName = _state.Value.Enemy.name;

                        _damageContainerPool.Value.Add(enemyEntity).DamageData = ListPoolService<TakeDamageData>.Get();

                        _enemyPool.Value.Add(enemyEntity);
                    }
                    else
                    {
                        ref var transformComp = ref _transformPool.Value.Get(enemyEntity);
                        transformComp.Transform.position = spawn;
                        transformComp.Transform.rotation = new Quaternion(0, 180, 0, 0);
                    }

                    if (UnityEngine.Random.value < 0.5f)
                    {
                        if (_state.Value.TryGetEntity("battlefield", out int entityBattleField))
                        {
                            ref var battleComp = ref _battlePool.Value.Get(entityBattleField);
                            ref var boundsComp = ref _boundsPool.Value.Add(enemyEntity);
                            boundsComp.MaxX = battleComp.MaxX;
                            boundsComp.MinX = battleComp.MinX;

                            ref var zigComp = ref _zigPool.Value.Add(enemyEntity);
                            zigComp.Amplitude = 2f;
                            zigComp.Frequency = 1f;
                            zigComp.Time = 1f;
                        }
                    }

                    addComp.TargetEntity.Add(enemyEntity);
                }
            }
        }
    }
}
