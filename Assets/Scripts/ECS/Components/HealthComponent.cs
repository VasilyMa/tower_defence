using Leopotam.EcsLite;
using Statement;

namespace Client
{
    struct HealthComponent : IRecycable
    {
        public float Value;
        public float MaxValue;

        public void Recycle(EcsWorld world, BattleState state, int entity)
        { 
        }
    }
}
