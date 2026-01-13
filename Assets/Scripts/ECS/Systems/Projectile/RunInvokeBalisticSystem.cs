using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Statement;
using UnityEngine;

namespace Client 
{
    sealed class RunInvokeBalisticSystem : IEcsRunSystem
    {
        readonly EcsFilterInject<Inc<InvokeEvent, BalisticComponent, ProjectileComponent, TransformComponent, DestinationComponent, MoveState>> _filter = default; 
        readonly EcsPoolInject<DestinationComponent> _destinationPool = default;
        readonly EcsPoolInject<TransformComponent> _tranformPool = default;
        readonly EcsPoolInject<BalisticComponent> _balisticPool = default;
        readonly EcsPoolInject<MoveState> _movePool = default;
        readonly EcsPoolInject<ActiveState> _activePool = default;

        readonly float heightFactor = 0.5f;

        public void Run (IEcsSystems systems) 
        {
            foreach (var entity in _filter.Value)
            {
                ref var transformComp = ref _tranformPool.Value.Get(entity);
                ref var destinationComp = ref _destinationPool.Value.Get(entity);
                ref var balisticComp = ref _balisticPool.Value.Get(entity);
                ref var moveComp = ref _movePool.Value.Get(entity);

                balisticComp.Start = transformComp.Transform.position;
                balisticComp.End = destinationComp.TargetPos;

                float distance = Vector3.Distance(balisticComp.Start, balisticComp.End);

                balisticComp.Height = distance * distance * heightFactor;

                Vector3 mid = (balisticComp.Start + balisticComp.End) * 0.5f;
                mid.y += balisticComp.Height;
                 
                balisticComp.Mid = mid;
                balisticComp.PrevPos = balisticComp.Start;
                balisticComp.Time = 0f; 

                balisticComp.Duration = distance / moveComp.Speed;

                transformComp.Transform.gameObject.SetActive(true);
                 
                _activePool.Value.Add(entity);
            }
        }
    }
}
