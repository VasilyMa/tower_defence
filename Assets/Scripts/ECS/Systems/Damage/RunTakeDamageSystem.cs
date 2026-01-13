using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Statement;

namespace Client 
{
    sealed class RunTakeDamageSystem : IEcsRunSystem 
    {
        readonly EcsSharedInject<BattleState> _state = default;
        readonly EcsFilterInject<Inc<DamageContainerComponent, ActiveState, HealthComponent, TransformComponent>> _filter = default;
        readonly EcsPoolInject<DamageContainerComponent> _damageContainerPool = default;
        readonly EcsPoolInject<HealthComponent> _healthPool = default;
        readonly EcsPoolInject<TransformComponent> _transformPool = default;    

        public void Run (IEcsSystems systems) 
        {
            foreach (var entity in _filter.Value)
            {
                ref var transformComp = ref _transformPool.Value.Get(entity);
                ref var damageContainerComp = ref _damageContainerPool.Value.Get(entity);
                ref var healthComp = ref _healthPool.Value.Get(entity);

                foreach (var damageData in damageContainerComp.DamageData)
                {
                    healthComp.Value -= damageData.Value;

                    _state.Value.InvokeDamageView(transformComp.Transform.position + UnityEngine.Vector3.up, damageData.Value);
                }
            }
        }
    }
}
