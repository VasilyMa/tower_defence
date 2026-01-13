using Leopotam.EcsLite;
using Statement;
using UnityEngine;

namespace Client 
{
    struct DestinationComponent : IRecycable
    {
        public Vector3 TargetPos;

        public void Recycle(EcsWorld world, BattleState state, int entity)
        { 

        }
    }
}
