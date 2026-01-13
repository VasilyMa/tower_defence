using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Client 
{
    sealed class RunClearDamageSystem : IEcsRunSystem 
    {
        readonly EcsFilterInject<Inc<DamageContainerComponent, ActiveState>> _filter = default;
        readonly EcsPoolInject<DamageContainerComponent> _damagePool = default;

        public void Run (IEcsSystems systems) 
        {
            foreach (var entity in _filter.Value)
            {
                ref var damageContainerComp = ref _damagePool.Value.Get(entity);
                damageContainerComp.Clear();
            }
        }
    }
}
