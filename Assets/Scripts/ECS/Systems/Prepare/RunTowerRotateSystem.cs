using Client;
using UnityEngine;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

sealed class RunTowerRotateSystem : IEcsRunSystem
{
    readonly EcsFilterInject<Inc<TowerComponent, TransformComponent, TargetComponent>, Exc<EmptyComponent>> _filter = default;

    readonly EcsPoolInject<TransformComponent> _transformPool = default;
    readonly EcsPoolInject<TargetComponent> _targetPool = default;
    readonly EcsPoolInject<TowerComponent> _towerPool = default;
    readonly EcsPoolInject<ReadyState> _readyPool = default;

    const float READY_ANGLE = 1f;
    const float UNREADY_ANGLE = 2f;

    public void Run(IEcsSystems systems)
    {
        foreach (var entity in _filter.Value)
        {
            ref var tower = ref _towerPool.Value.Get(entity);
            ref var towerTr = ref _transformPool.Value.Get(entity);
            ref var target = ref _targetPool.Value.Get(entity);
            ref var targetTr = ref _transformPool.Value.Get(target.TargetEntity);

            Vector3 dir = targetTr.Transform.position - towerTr.Transform.position;
            dir.y = 0f;

            if (dir.sqrMagnitude < 0.0001f)
                continue;

            dir.Normalize();
             
            Quaternion targetRot = Quaternion.LookRotation(dir);
            float t = Mathf.Clamp01(tower.RotationSpeed * Time.deltaTime);
            towerTr.Transform.rotation = Quaternion.Slerp(towerTr.Transform.rotation, targetRot, t);
             
            Vector3 forward = towerTr.Transform.forward;
            forward.y = 0f;
            forward.Normalize();

            float angle = Vector3.Angle(forward, dir);

            bool hasReady = _readyPool.Value.Has(entity);

            if (!hasReady && angle <= READY_ANGLE)
            {
                _readyPool.Value.Add(entity);
            }
            else if (hasReady && angle >= UNREADY_ANGLE)
            {
                _readyPool.Value.Del(entity);
            }
        }
    }
}
