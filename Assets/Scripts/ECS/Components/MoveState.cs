using Leopotam.EcsLite;
using Statement; 

namespace Client 
{
    struct MoveState : IRecycable
    { 
        public float Speed;
        public void Recycle(EcsWorld world, BattleState state, int entity)
        {

        }
    }
}
