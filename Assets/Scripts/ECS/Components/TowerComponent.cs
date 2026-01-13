using UnityEngine;

namespace Client
{
    struct TowerComponent 
    {
        public float RotationSpeed;
        public GameObject ProjectilePrefView;
        public Transform Holder;
        public Transform FirePoint;
        public float Range;
        public float Cooldown;  
    }
}
