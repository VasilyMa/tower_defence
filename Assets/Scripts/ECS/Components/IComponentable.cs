using Leopotam.EcsLite;
using Statement;

namespace Client 
{
    interface IComponentable 
    {
        void AddComponent(EcsWorld world, BattleState state, int entity);
    }
}
