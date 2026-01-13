using Leopotam.EcsLite;
using Statement;

namespace Client 
{
    struct VelocityComponent : IComponentable
    {
        public float Value;

        public void AddComponent(EcsWorld world, BattleState state, int entity)
        {
            ref var moveComp = ref world.GetPool<MoveState>().Add(entity);
            moveComp.Speed = Value;
        }
    }
}
