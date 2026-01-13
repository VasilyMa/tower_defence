using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Statement;

namespace Client 
{
    sealed class RunTowerAttackSystem : IEcsRunSystem 
    {
        readonly EcsWorldInject _world = default;
        readonly EcsFilterInject<Inc<TowerComponent, TargetComponent, ReadyState>, Exc<EmptyComponent, InCooldownState>> _filter = default; 
        readonly EcsPoolInject<ProjectileComponent> _projectilePool = default;
        readonly EcsPoolInject<AddComponentEvent> _addComponentPool = default;
        readonly EcsPoolInject<TowerComponent> _towerPool = default;
        readonly EcsPoolInject<TransformComponent> _transformPool = default;
        readonly EcsPoolInject<InCooldownState> _cooldownPool = default;
        readonly EcsPoolInject<PoolComponent> _pool = default;

        public void Run (IEcsSystems systems) 
        {
            foreach (var entity in _filter.Value)
            { 
                ref var towerComp = ref _towerPool.Value.Get(entity); 

                int projectileEntity = default;

                if (!EntityPoolService.TryGet(towerComp.ProjectilePrefView.name, out projectileEntity))
                {
                    projectileEntity = _world.Value.NewEntity();

                    var projectileInstance = UnityEngine.Object.Instantiate(towerComp.ProjectilePrefView, towerComp.FirePoint.position, towerComp.FirePoint.rotation);

                    ref var transfrormComp = ref _transformPool.Value.Add(projectileEntity);
                    transfrormComp.Transform = projectileInstance.transform;

                    _pool.Value.Add(projectileEntity).PoolKeyName = towerComp.ProjectilePrefView.name;

                    _projectilePool.Value.Add(projectileEntity);
                }
                else
                {
                    ref var transformComp = ref _transformPool.Value.Get(projectileEntity);
                    transformComp.Transform.position = towerComp.FirePoint.position;
                    transformComp.Transform.rotation = towerComp.FirePoint.rotation;
                }

                ref var addComp = ref _addComponentPool.Value.Add(entity);
                addComp.TargetEntity = ListPoolService<int>.Get();
                addComp.TargetEntity.Add(projectileEntity);

                _cooldownPool.Value.Add(entity).Cooldown = towerComp.Cooldown;
            }
        }
    }
}
