using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Statement;
using UnityEngine;


namespace Client 
{
    sealed class RunSpawnerSystem : IEcsRunSystem 
    {
        readonly EcsWorldInject _world = default;
        readonly EcsSharedInject<BattleState> _state = default;
        readonly EcsFilterInject<Inc<SpawnerComponent>> _filter = default;
        readonly EcsPoolInject<SpawnerComponent> _spawnerPool = default;
        readonly EcsPoolInject<SpawnEvent> _spawnEvent = default;

        public void Run (IEcsSystems systems) 
        {
            foreach (var entity in _filter.Value)
            {
                ref var spawnerComp = ref _spawnerPool.Value.Get(entity);

                spawnerComp.SpawnDelay -= Time.deltaTime;

                if (spawnerComp.SpawnDelay < 0)
                {
                    spawnerComp.SpawnDelay = spawnerComp.GameConfig.SpawnTime;
                     
                    ref var spawnEvent = ref _spawnEvent.Value.Add(entity);
                    spawnEvent.Count = spawnerComp.GameConfig.SpawnCount;
                    spawnEvent.SpawnPoint = new System.Collections.Generic.List<Vector3>(spawnerComp.SpawnPoints);
                }
            }
        }
    }
}
