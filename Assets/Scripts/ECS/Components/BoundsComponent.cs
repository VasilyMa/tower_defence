using Leopotam.EcsLite;
using Statement;

namespace Client 
{
    struct BoundsComponent : IRecycable
    {
        public float MinX;
        public float MaxX;

        public void Recycle(EcsWorld world, BattleState state, int entity)
        { 
        }
    }
}
