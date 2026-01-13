using Leopotam.EcsLite;
using Statement;

namespace Client 
{
    struct ActiveState : IRecycable, INullable<TransformComponent>
    {
        public void Nullable(ref TransformComponent component)
        { 
            component.Transform.gameObject.SetActive(false);
        }

        public void Recycle(EcsWorld world, BattleState state, int entity)
        {

        }
    }
}
