using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Client 
{
    sealed class RunCooldownCompleteSystem : IEcsRunSystem 
    {
        readonly EcsFilterInject<Inc<InCooldownState>> _filter = default;
        readonly EcsPoolInject<InCooldownState> _cooldownPool = default;

        public void Run (IEcsSystems systems) 
        {
            foreach (var entity in _filter.Value)
            {
                ref var cooldownComp = ref _cooldownPool.Value.Get(entity);
                
                if (cooldownComp.Cooldown < 0)
                {
                    _cooldownPool.Value.Del(entity);
                }
            }
        }
    }
}
