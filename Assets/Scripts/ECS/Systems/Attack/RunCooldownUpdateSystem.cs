using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client 
{
    sealed class RunCooldownUpdateSystem : IEcsRunSystem 
    {
        readonly EcsWorldInject _world = default;
        readonly EcsFilterInject<Inc<InCooldownState>> _filter = default;
        readonly EcsPoolInject<InCooldownState> _cooldownPool = default;

        public void Run (IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                ref var cooldownComp = ref _cooldownPool.Value.Get(entity);
                cooldownComp.Cooldown -= Time.deltaTime;
            }
        }
    }
}
