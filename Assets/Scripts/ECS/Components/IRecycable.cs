using Leopotam.EcsLite;
using Statement;

namespace Client 
{
    interface IRecycable 
    {
        void Recycle(EcsWorld world, BattleState state, int entity);   
    }
}
