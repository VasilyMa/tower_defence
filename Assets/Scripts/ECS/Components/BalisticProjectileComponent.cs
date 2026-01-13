using Leopotam.EcsLite;
using Statement;

namespace Client 
{
    struct BalisticProjectileComponent : IComponentable
    {
        public float Radius;
        public float Height;

        public void AddComponent(EcsWorld world, BattleState state, int entity)
        {
            ref var balistComp = ref world.GetPool<BalisticComponent>().Add(entity);
            balistComp.Radius = Radius;
            balistComp.Height = Height;
        }
    }
}
