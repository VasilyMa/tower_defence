using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Client 
{
    sealed class RunRecycleCompleteSystem : IEcsRunSystem
    {
        readonly EcsWorldInject _world = default;
        readonly EcsFilterInject<Inc<RecycleEvent, PoolComponent>> _filter = default;
        readonly EcsPoolInject<PoolComponent> _pool = default;

        public void Run (IEcsSystems systems) 
        {
            foreach (var entity in _filter.Value)
            {
                ref var poolComp = ref _pool.Value.Get(entity);

                EntityPoolService.Release(poolComp.PoolKeyName, entity);
            }
        }
    }
}
