using Leopotam.EcsLite;
using Statement;

namespace Client 
{
    struct TargetComponent : IComponentable
    {
        public int TargetEntity;

        public void AddComponent(EcsWorld world, BattleState state, int entity)
        {
            ref var targetTransformComp = ref world.GetPool<TransformComponent>().Get(TargetEntity);
            ref var destinatioComp = ref world.GetPool<DestinationComponent>().Add(entity);
            destinatioComp.TargetPos = targetTransformComp.Transform.position;
        }
    }
}
