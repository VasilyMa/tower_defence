using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Statement;
using UnityEngine;

namespace Client 
{
    sealed class InitSpawnerSystem : IEcsInitSystem 
    {
        readonly EcsWorldInject _world = default;
        readonly EcsSharedInject<BattleState> _state = default;
        readonly EcsPoolInject<SpawnerComponent> _spawnerPool = default;
        readonly EcsPoolInject<MovementComponent> _movePool = default;
        readonly EcsPoolInject<DurabilityComponent> _durabilityPool = default;

        public void Init (IEcsSystems systems) 
        {
            var entity = _world.Value.NewEntity();

            ref var spawnerComp = ref _spawnerPool.Value.Add(entity);

            var points = GameObject.FindObjectsOfType<SpawnPoint>();

            spawnerComp.SpawnPoints = new Vector3[points.Length];

            for (int i = 0; i < points.Length; i++)
            {
                spawnerComp.SpawnPoints[i] = points[i].transform.position;
            }
             
            spawnerComp.SpawnDelay = 1f;
            spawnerComp.GameConfig = _state.Value.GameConfig;

            ref var moveComp = ref _movePool.Value.Add(entity);  
            ref var durabilityComp = ref _durabilityPool.Value.Add(entity); 

            _state.Value.AddEntity("spawner", entity);
        }
    }
}
