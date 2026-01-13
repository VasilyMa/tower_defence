using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Client 
{
    sealed class RunResolveSystem : IEcsRunSystem
    {
        readonly EcsFilterInject<Inc<ResolveEvent>> _filter = default;
        readonly EcsPoolInject<RecycleEvent> _recyclePool = default;
        public void Run (IEcsSystems systems) 
        {
            foreach (var entity in _filter.Value)
            {
                _recyclePool.Value.Add(entity);
            }
        }
    }
}
