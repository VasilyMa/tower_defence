using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Statement;

namespace Client
{
    sealed class RunDieSystem : IEcsRunSystem
    {
        readonly EcsSharedInject<BattleState> _state = default;
        readonly EcsFilterInject<Inc<ActiveState, HealthComponent, TransformComponent>> _filter = default;
        readonly EcsPoolInject<HealthComponent> _healthPool = default;
        readonly EcsPoolInject<RecycleEvent> _recyclePool = default;
        readonly EcsPoolInject<TransformComponent> _transformPool = default;

        public void Run (IEcsSystems systems) 
        {
            foreach (var entity in _filter.Value)
            {
                ref var healthComp = ref _healthPool.Value.Get(entity);

                if (healthComp.Value < 0)
                {
                    _state.Value.AddCurrency(1);

                    _transformPool.Value.Get(entity).Transform.gameObject.SetActive(false);

                    _recyclePool.Value.Add(entity);
                }
            }
        }
    }
}
