using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client 
{
    sealed class RunMovementEnemySystem : IEcsRunSystem 
    {  
        readonly EcsFilterInject<Inc<EnemyComponent, TransformComponent, MoveState, ActiveState>> _filter = default;
        readonly EcsPoolInject<TransformComponent> _transformPool = default;
        readonly EcsPoolInject<MoveState> _movePool = default;
        readonly EcsPoolInject<RecycleEvent> _recyclePool = default;

        public void Run (IEcsSystems systems) 
        {
            foreach (var entity in _filter.Value)
            {
                ref var transformComp = ref _transformPool.Value.Get(entity);
                ref var moveComp = ref _movePool.Value.Get(entity);

                transformComp.Transform.position += transformComp.Transform.forward * moveComp.Speed * Time.fixedDeltaTime;

                if (transformComp.Transform.position.z <= 0)
                {
                    transformComp.Transform.gameObject.SetActive(false);

                    _recyclePool.Value.Add(entity);
                }
            }
        }
    }
}
