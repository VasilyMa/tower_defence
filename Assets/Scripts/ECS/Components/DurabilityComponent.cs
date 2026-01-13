using Leopotam.EcsLite;
using Statement;

namespace Client 
{
    struct DurabilityComponent : IComponentable
    { 
        public void AddComponent(EcsWorld world, BattleState state, int entity)
        {
            ref var healthComp = ref world.GetPool<HealthComponent>().Add(entity);
            healthComp.Value = state.GameConfig.Health;
            healthComp.MaxValue = state.GameConfig.Health;
        }
    }
}
