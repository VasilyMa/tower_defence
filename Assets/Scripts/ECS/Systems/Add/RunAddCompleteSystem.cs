using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Client 
{
    sealed class RunAddCompleteSystem : IEcsRunSystem
    {
        readonly EcsWorldInject _world = default;
        readonly EcsFilterInject<Inc<AddComponentEvent>> _filter = default; 
        readonly EcsPoolInject<AddComponentEvent> _addPool = default;
        readonly EcsPoolInject<InvokeEvent> _invokePool = default;

        public void Run (IEcsSystems systems) 
        {
            foreach (var entity in _filter.Value)
            {
                ref var addEventComp = ref _addPool.Value.Get(entity);

                foreach (var targetEntity in addEventComp.TargetEntity)
                { 
                    _invokePool.Value.Add(targetEntity);
                }

                ListPoolService<int>.Release(addEventComp.TargetEntity);
            }
        }
    }
}
