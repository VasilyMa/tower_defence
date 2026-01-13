using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Statement;
using UnityEngine;

namespace Client 
{
    sealed class RunMotionBalisticSystem : IEcsRunSystem 
    {
        readonly EcsWorldInject _world = default;
        readonly EcsSharedInject<BattleState> _state = default;
        readonly EcsFilterInject<Inc<ProjectileComponent, BalisticComponent, ActiveState, MoveState, TransformComponent>> _filter = default;
        readonly EcsPoolInject<TransformComponent> _transformPool = default;
        readonly EcsPoolInject<BalisticComponent> _balisticPool = default;
        readonly EcsPoolInject<MoveState> _movePool = default;
        readonly EcsPoolInject<ResolveEvent> _resolvePool = default;

        int _mask = LayerMask.GetMask("Enemy");

        Collider[] _colliders = new Collider[10];

        public void Run (IEcsSystems systems) 
        {
            foreach (var entity in _filter.Value)
            {
                ref var balisticComp = ref _balisticPool.Value.Get(entity);
                ref var transformComp =ref _transformPool.Value.Get(entity);
                ref var moveComp =ref _movePool.Value.Get(entity); 

                balisticComp.Time += Time.deltaTime * moveComp.Speed;

                float t = Mathf.Clamp01(balisticComp.Time / balisticComp.Duration);

                Vector3 pos = Ballistic(balisticComp.Start, balisticComp.Mid, balisticComp.End, t);

                transformComp.Transform.position = pos;
                 
                Vector3 velocity = (pos - balisticComp.PrevPos) / Time.deltaTime;

                if (velocity.sqrMagnitude > 0.0001f) transformComp.Transform.rotation = Quaternion.LookRotation(velocity);

                balisticComp.PrevPos = pos;

                if (balisticComp.Time >= balisticComp.Duration)
                {
                    transformComp.Transform.gameObject.SetActive(false);

                    int hits = Physics.OverlapSphereNonAlloc(transformComp.Transform.position, balisticComp.Radius, _colliders, _mask);

                    if (hits > 0)
                    {
                        ref var resolveComp = ref _resolvePool.Value.Add(entity); 
                        resolveComp.Entities = ListPoolService<int>.Get();

                        for (global::System.Int32 i = 0; i < hits; i++)
                        {
                            if (_state.Value.TryGetEntity(_colliders[i].transform.name, out int targetEntity))
                            {
                                resolveComp.Entities.Add(targetEntity);
                            }
                        }
                    }
                }
            }
        }

        private Vector3 Ballistic(Vector3 start, Vector3 apex, Vector3 end, float t)
        {
            Vector3 pos = Vector3.Lerp(start, end, t);
            pos.y = Parabola(start.y, apex.y, end.y, t);
            return pos;
        }

        private float Parabola(float y0, float y1, float y2, float t)
        {
            float a = Mathf.Lerp(y0, y1, t);
            float b = Mathf.Lerp(y1, y2, t);
            return Mathf.Lerp(a, b, t);
        }
    }
}
