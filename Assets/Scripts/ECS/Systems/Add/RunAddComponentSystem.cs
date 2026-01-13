using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Statement;
using UnityEngine;

namespace Client 
{
    sealed class RunAddComponentSystem<T> : IEcsRunSystem where T : struct, IComponentable
    {
        readonly EcsWorldInject _world = default;
        readonly EcsSharedInject<BattleState> _state = default;
        readonly EcsFilterInject<Inc<AddComponentEvent, T>> _filter = default;
        readonly EcsPoolInject<T> _pool = default;
        readonly EcsPoolInject<AddComponentEvent> _eventPool = default;
        public void Run (IEcsSystems systems) 
        {
            foreach (var entity in _filter.Value)
            { 
                ref var comp = ref _pool.Value.Get(entity);
                ref var addComp = ref _eventPool.Value.Get(entity);

                foreach (var targetEntity in addComp.TargetEntity)
                { 
                    comp.AddComponent(_world.Value, _state.Value, targetEntity);
                } 
            }
        }
    }
}
