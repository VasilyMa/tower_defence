using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Statement;
using UnityEngine;

namespace Client
{
    sealed class InitBattleFieldSystem : IEcsInitSystem
    {
        readonly EcsWorldInject _world = default;
        readonly EcsSharedInject<BattleState> _state = default;
        readonly EcsPoolInject<BattlefieldComponent> _battlePool = default;

        public void Init (IEcsSystems systems) 
        {
            var entity = _world.Value.NewEntity ();

            var battlefield = GameObject.FindObjectOfType<BattleField>();

            ref var battleFieldComp = ref _battlePool.Value.Add(entity);
            battleFieldComp.MinX = battlefield.Min.x;
            battleFieldComp.MaxX = battlefield.Max.x;

            _state.Value.AddEntity("battlefield", entity);
        }
    }
}
