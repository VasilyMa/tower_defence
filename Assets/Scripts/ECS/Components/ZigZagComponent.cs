using Leopotam.EcsLite;
using Statement;

namespace Client 
{
    struct ZigZagComponent : IRecycable
    {
        public float Amplitude;   // ширина зигзага
        public float Frequency;   // скорость колебаний
        public float Time;

        public void Recycle(EcsWorld world, BattleState state, int entity)
        { 

        }
    }
}
