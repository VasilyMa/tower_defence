using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Statement;

namespace Client
{
    sealed class RunNullableSystem<TState, TComponent> : IEcsRunSystem where TState : struct, INullable<TComponent> where TComponent : struct
    {
        readonly EcsFilterInject<Inc<TState, NullableEvent<TState>>> _filter = default;
        readonly EcsPoolInject<TState> _statePool = default;
        readonly EcsPoolInject<TComponent> _compPool = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                ref var state = ref _statePool.Value.Get(entity);
                ref var component = ref _compPool.Value.Get(entity);

                state.Nullable(ref component);

                _statePool.Value.Del(entity);
            }
        }
    }
}