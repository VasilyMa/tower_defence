using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Statement;
using UnityEngine;

namespace Client 
{
    sealed class RunTowerAimSystem : IEcsRunSystem 
    {  
        readonly EcsFilterInject<Inc<TowerComponent, TransformComponent>, Exc<EmptyComponent>> _filterTower = default;
        readonly EcsFilterInject<Inc<EnemyComponent, TransformComponent, ActiveState>> _filterEnemies = default;
        readonly EcsPoolInject<TransformComponent> _transformPool = default;
        readonly EcsPoolInject<TargetComponent> _targetPool = default;
        readonly EcsPoolInject<TowerComponent> _towerPool = default;

        public void Run (IEcsSystems systems) 
        {
            if (_filterEnemies.Value.GetEntitiesCount() == 0) return;

            foreach (var entityTower in _filterTower.Value)
            {
                ref var towerComp = ref _towerPool.Value.Get(entityTower);
                ref var towerTransformComp = ref _transformPool.Value.Get(entityTower);

                int targetEntity = -1;
                float minSqrDistance = towerComp.Range * towerComp.Range; 

                Vector3 towerPos = towerTransformComp.Transform.position;
                towerPos.y = 0f;

                foreach (var entityEnemy in _filterEnemies.Value)
                {
                    ref var enemyTransformComp = ref _transformPool.Value.Get(entityEnemy);

                    Vector3 enemyPos = enemyTransformComp.Transform.position;
                    enemyPos.y = 0f;

                    float sqrDistance = (towerPos - enemyPos).sqrMagnitude;

                    if (sqrDistance < minSqrDistance)
                    {
                        minSqrDistance = sqrDistance;
                        targetEntity = entityEnemy;
                    }
                }

                if (targetEntity == -1)
                {
                    _targetPool.Value.Del(entityTower);
                    continue;
                }

                if(!_targetPool.Value.Has(entityTower)) _targetPool.Value.Add(entityTower);

                ref var targetComp = ref _targetPool.Value.Get(entityTower);
                targetComp.TargetEntity = targetEntity;
            }
        }
    }
}
