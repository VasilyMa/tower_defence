using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client 
{
    sealed class RunInvokeEnemySystem : IEcsRunSystem 
    {
        readonly EcsFilterInject<Inc<InvokeEvent, EnemyComponent, TransformComponent>> _filter = default;
        readonly EcsPoolInject<TransformComponent> _transformPool = default;
        readonly EcsPoolInject<ActiveState> _activePool = default;

        public void Run (IEcsSystems systems) 
        {
            foreach (var entity in _filter.Value)
            {
                ref var transformComp = ref _transformPool.Value.Get(entity);
                transformComp.Transform.gameObject.SetActive(true);

                _activePool.Value.Add(entity);
            }
        }
    }
}
