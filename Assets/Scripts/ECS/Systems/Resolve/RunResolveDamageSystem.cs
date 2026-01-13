using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Client 
{
    sealed class RunResolveDamageSystem : IEcsRunSystem 
    {
        readonly EcsWorldInject _world = default;
        readonly EcsFilterInject<Inc<ResolveEvent, DamageState>> _filter = default;
        readonly EcsPoolInject<ResolveEvent> _resolvePool = default;
        readonly EcsPoolInject<DamageState> _damagePool = default;
        readonly EcsPoolInject<DamageContainerComponent> _damageContainerPool = default;

        public void Run (IEcsSystems systems) 
        {
            foreach (var e in _filter.Value)
            {
                ref var resolveComp = ref _resolvePool.Value.Get(e);
                ref var damageComp = ref _damagePool.Value.Get(e);

                foreach (var entity in resolveComp.Entities)
                {
                    ref var damageContainerComp = ref _damageContainerPool.Value.Get(entity);
                    damageContainerComp.DamageData.Add(new TakeDamageData(damageComp.Value));
                }
            }
        }
    }
}
