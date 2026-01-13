using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client
{
    sealed class RunInvokeArrowSystem : IEcsRunSystem 
    {
        readonly EcsWorldInject _world = default; 
        readonly EcsFilterInject<Inc<InvokeEvent, ArrowComponent, ProjectileComponent, TransformComponent, DestinationComponent, MoveState>> _filter = default; 
        readonly EcsPoolInject<DestinationComponent> _destinationPool = default;
        readonly EcsPoolInject<TransformComponent> _tranformPool = default; 
        readonly EcsPoolInject<ActiveState> _activePool = default;
        readonly EcsPoolInject<ArrowComponent> _arrowPool = default;
        readonly EcsPoolInject<MoveState> _movePool = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                ref var transformComp = ref _tranformPool.Value.Get(entity);
                ref var destinationComp = ref _destinationPool.Value.Get(entity);

                Vector3 direction = destinationComp.TargetPos - transformComp.Transform.position;
                direction.y = 0f;

                if (direction.sqrMagnitude > 0.0001f)
                {
                    transformComp.Transform.rotation = Quaternion.LookRotation(direction);
                }

                ref var moveComp = ref _movePool.Value.Get(entity);
                ref var arrowComp = ref _arrowPool.Value.Get(entity);

                arrowComp.Delay = Vector3.Distance(destinationComp.TargetPos, transformComp.Transform.position) / moveComp.Speed;

                transformComp.Transform.gameObject.SetActive(true);
                 
                _activePool.Value.Add(entity);
            }
        }
    }
}
