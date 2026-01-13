using Leopotam.EcsLite;
using Statement;

namespace Client 
{
    struct ArrowComponent : IRecycable
    {
        public float Delay;

        public void Recycle(EcsWorld world, BattleState state, int entity)
        { 
        }
    }
}
