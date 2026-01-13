using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client 
{
    sealed class RunSpawnTowerSystem : IEcsRunSystem
    {
        readonly EcsFilterInject<Inc<SpawnTowerEvent>> _filter = default;
        readonly EcsFilterInject<Inc<TowerComponent, EmptyComponent>> _filterTower = default;
        readonly EcsPoolInject<SpawnTowerEvent> _spawnPool = default;
        readonly EcsPoolInject<EmptyComponent> _emptyPool = default;
        readonly EcsPoolInject<TransformComponent> _transformPool = default;
        readonly EcsPoolInject<TowerComponent> _towerPool = default;
        readonly EcsPoolInject<DamageComponent> _damagePool = default;
        readonly EcsPoolInject<ArrowProjectileComponent> _arrowPool = default;
        readonly EcsPoolInject<BalisticProjectileComponent> _balisticPool = default;
        readonly EcsPoolInject<VelocityComponent> _velocityPool = default;

        public void Run (IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                ref var spawnComp = ref _spawnPool.Value.Get(entity);

                foreach (var entityTower in _filterTower.Value)
                {
                    ref var towerComp = ref _towerPool.Value.Get(entityTower);
                    var towerInstance = GameObject.Instantiate(spawnComp.Tower.ViewObject, towerComp.Holder);

                    ref var transformComp = ref _transformPool.Value.Add(entityTower);
                    transformComp.Transform = towerInstance.transform;

                    towerComp.FirePoint = transformComp.Transform.GetChild(0);

                    towerComp.ProjectilePrefView = spawnComp.Tower.ProjectileObject;

                    towerComp.Cooldown = spawnComp.Tower.Speed;

                    towerComp.Range = spawnComp.Tower.Range;

                    towerComp.RotationSpeed = spawnComp.Tower.Rotation;

                    ref var damageComp = ref _damagePool.Value.Add(entityTower);
                    damageComp.Value = spawnComp.Tower.Damage;

                    ref var velocityComp = ref _velocityPool.Value.Add(entityTower);
                    velocityComp.Value = spawnComp.Tower.MissileSpeed;
                     
                    switch (spawnComp.Tower.Type)
                    {
                        case TowerType.archer:
                            _arrowPool.Value.Add(entityTower);
                            break;
                        case TowerType.launcher:
                            ref var balisticComp = ref _balisticPool.Value.Add(entityTower);
                            balisticComp.Radius = spawnComp.Tower.Radius;
                            balisticComp.Height = spawnComp.Tower.Height;
                            break;
                    }

                    _emptyPool.Value.Del(entityTower);  
                    break;
                }
            }
        }
    }
}
