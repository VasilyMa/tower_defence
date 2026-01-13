using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Statement;
using UnityEngine;

namespace Client 
{
    sealed class RunTowerRotateCompleteSystem : IEcsRunSystem 
    { 
        readonly EcsFilterInject<Inc<TowerComponent, TransformComponent, TargetComponent, ReadyState>, Exc<EmptyComponent>> _filter = default;
        readonly EcsPoolInject<TransformComponent> _transformPool = default;
        readonly EcsPoolInject<TargetComponent> _targetPool = default; 
        readonly EcsPoolInject<ReadyState> _readyPool = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                ref var targetComp = ref _targetPool.Value.Get(entity);
                ref var towerTransformComp = ref _transformPool.Value.Get(entity);
                ref var targetTransformComp = ref _transformPool.Value.Get(targetComp.TargetEntity);
                Vector3 direction = targetTransformComp.Transform.position - towerTransformComp.Transform.position;
                direction.y = 0f;
                direction.Normalize();

                Vector3 forward = towerTransformComp.Transform.forward;
                forward.y = 0f;
                forward.Normalize();

                float angle = Vector3.Angle(forward, direction);

                if (angle > 2f)
                {
                    _readyPool.Value.Del(entity);
                }
            }
        }
    }
}
