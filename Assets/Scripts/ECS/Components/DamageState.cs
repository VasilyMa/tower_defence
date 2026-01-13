using Leopotam.EcsLite;
using Statement;

namespace Client
{
    struct DamageState : IRecycable
    {
        public float Value; 

        public void Recycle(EcsWorld world, BattleState state, int entity)
        {

        }
    }
}
