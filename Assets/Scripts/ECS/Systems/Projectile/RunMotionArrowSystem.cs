using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Statement;
using UnityEngine;

namespace Client 
{
    sealed class RunMotionArrowSystem : IEcsRunSystem 
    {
        readonly EcsSharedInject<BattleState> _state = default;
        readonly EcsFilterInject<Inc<ProjectileComponent, ArrowComponent, ActiveState, MoveState, TransformComponent>> _filter = default;
        readonly EcsPoolInject<TransformComponent> _transformPool = default;
        readonly EcsPoolInject<ArrowComponent> _arrowPool = default;
        readonly EcsPoolInject<MoveState> _movePool = default;
        readonly EcsPoolInject<ResolveEvent> _resolvePool = default;
        readonly EcsPoolInject<RecycleEvent> _recyclePool = default;
         

        RaycastHit hit;

        int _mask = LayerMask.GetMask("Enemy");

        public void Run (IEcsSystems systems) 
        {
            foreach (var entity in _filter.Value)
            {
                ref var transformComp = ref _transformPool.Value.Get(entity);
                ref var moveComp = ref _movePool.Value.Get(entity);
                ref var arrowComp = ref _arrowPool.Value.Get(entity);
                arrowComp.Delay -= Time.deltaTime;

                if (arrowComp.Delay < 0)
                {
                    transformComp.Transform.gameObject.SetActive(false); 
                    _recyclePool.Value.Add(entity); 
                    continue;
                }

                transformComp.Transform.position += transformComp.Transform.forward * moveComp.Speed * Time.fixedDeltaTime;

                Ray ray = new Ray(transformComp.Transform.position, transformComp.Transform.forward);

                if (Physics.Raycast(ray, out hit, 0.2f, _mask))
                {
                    if (_state.Value.TryGetEntity(hit.transform.name, out int targetEntity))
                    {
                        ref var resolveComp = ref _resolvePool.Value.Add(entity);

                        resolveComp.Entities = ListPoolService<int>.Get();
                        resolveComp.Entities.Add(targetEntity);

                        transformComp.Transform.gameObject.SetActive(false);
                    }
                }
            }
        }
    }
}
