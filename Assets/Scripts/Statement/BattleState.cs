using Leopotam.EcsLite;
using System.Collections.Generic; 
using UnityEngine;

namespace Statement
{
    public class BattleState : State
    {
        public static new BattleState Instance
        {
            get
            {
                return (BattleState)State.Instance;
            }
        }

        [HideInInspector] public EcsRunHandler EcsHandler;
        protected Dictionary<string, EcsPackedEntity> _entityMap = new();

        public override void Awake()
        { 
            EcsHandler = new EcsRunHandler(this);
        }

        public override void Start() => EcsHandler?.Init(); 
        public override void Update() => EcsHandler.Run();
        public override void LateUpdate() => EcsHandler.AfterRun();
        public override void FixedUpdate() => EcsHandler.FixedRun();
        public override void OnDestroy()
        {
            EcsHandler.Dispose(); 
        } 

        public void RemoveEntity(string entity)
        {
            _entityMap.Remove(entity);
        }

        public void AddEntity(string localKey, int entity)
        {
            var packed = EcsHandler.PackEntity(entity);

            if (!string.IsNullOrEmpty(localKey) && !_entityMap.ContainsKey(localKey))
                _entityMap[localKey] = packed;
        }

        public bool TryGetPlayer(out int playerEntity)
        {
            if (TryGetEntity("player", out playerEntity))
            {
                return true;
            }

            return false;
        }

        public bool TryGetEntity(string key, out EcsPackedEntity packedEntity)
        {
            packedEntity = default;

            if (string.IsNullOrEmpty(key)) return false; 

            return _entityMap.TryGetValue(key, out packedEntity);
        }

        public bool TryGetEntity(string key, out int unpackedEntity)
        {
            if (TryGetEntity(key, out EcsPackedEntity packed) && EcsHandler.Unpack(packed, out int entity))
            {
                unpackedEntity = entity;
                return true;
            }

            unpackedEntity = -1;
            return false;
        }  
    }
}