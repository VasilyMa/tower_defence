using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client 
{
    sealed class RunOffsetEnemySystem : IEcsRunSystem
    {
        readonly EcsFilterInject<Inc<EnemyComponent, TransformComponent, ZigZagComponent, BoundsComponent, ActiveState>> _filter = default;

        readonly EcsPoolInject<TransformComponent> _transformPool = default;
        readonly EcsPoolInject<ZigZagComponent> _zigzagPool = default;
        readonly EcsPoolInject<BoundsComponent> _boundsPool = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                ref var transformComp = ref _transformPool.Value.Get(entity);
                ref var zigzagComp = ref _zigzagPool.Value.Get(entity);
                ref var boundsComp = ref _boundsPool.Value.Get(entity);

                zigzagComp.Time += Time.fixedDeltaTime;

                float offsetX = Mathf.Sin(zigzagComp.Time * zigzagComp.Frequency) * zigzagComp.Amplitude;

                Vector3 pos = transformComp.Transform.position;
                pos.x += offsetX * Time.fixedDeltaTime;

                // Ограничиваем зону
                pos.x = Mathf.Clamp(pos.x, boundsComp.MinX, boundsComp.MaxX);

                transformComp.Transform.position = pos;
            }
        }
    }
}
