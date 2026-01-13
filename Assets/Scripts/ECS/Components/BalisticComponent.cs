using Leopotam.EcsLite;
using Statement;
using UnityEngine;

namespace Client 
{
    struct BalisticComponent : IRecycable
    {
        public Vector3 Start;
        public Vector3 End;
        public Vector3 Mid;

        public float Duration;
        public float Time;

        public float Radius;
        public float Height;

        public Vector3 PrevPos;

        public void Recycle(EcsWorld world, BattleState state, int entity)
        { 

        }
    }
}
