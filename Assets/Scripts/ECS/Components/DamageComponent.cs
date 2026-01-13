using Leopotam.EcsLite;
using Statement;

namespace Client 
{
    struct DamageComponent : IComponentable
    {
        public float Value;

        public void AddComponent(EcsWorld world, BattleState state, int entity)
        {
            ref var damgeComp = ref world.GetPool<DamageState>().Add(entity);
            damgeComp.Value = Value;
        }
    }
}
