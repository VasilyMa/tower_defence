using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Statement;
using System;
using UnityEngine;

namespace Client 
{
    sealed class InitTowerSystem : IEcsInitSystem
    {
        readonly EcsWorldInject _world = default;
        readonly EcsSharedInject<BattleState> _state = default;
        readonly EcsPoolInject<TowerComponent> _towerPool = default;
        readonly EcsPoolInject<EmptyComponent> _emptyPool = default;

        public void Init (IEcsSystems systems) 
        {
            var points = GameObject.FindObjectsOfType<TowerPoint>();

            int index = 0;

            foreach (var point in points)
            {
                var entity = _world.Value.NewEntity();

                var entityKey = $"tower_{index}";

                ref var towerComp = ref _towerPool.Value.Add(entity);
                towerComp.Holder = point.Holder; 

                _emptyPool.Value.Add(entity);

                _state.Value.AddEntity(entityKey, entity);

                index++;
            }
        }
    }
}
