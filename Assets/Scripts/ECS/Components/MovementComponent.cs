using Leopotam.EcsLite;
using Statement;

namespace Client 
{
    struct MovementComponent : IComponentable
    { 
        public void AddComponent(EcsWorld world, BattleState state, int entity)
        {
            ref var moveComp = ref world.GetPool<MoveState>().Add(entity);
            moveComp.Speed = state.GameConfig.MoveSpeed;
        }
    }
}
