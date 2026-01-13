using Leopotam.EcsLite;
using Statement;

namespace Client 
{
    struct ArrowProjectileComponent : IComponentable
    {  
        public void AddComponent(EcsWorld world, BattleState state, int entity)
        {
            ref var arrowComp = ref world.GetPool<ArrowComponent>().Add(entity);
        }
    }
}
