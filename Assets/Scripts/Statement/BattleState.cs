using Client;
using DamageNumbersPro;
using Leopotam.EcsLite;
using System;
using System.Collections.Generic; 
using UnityEngine;

namespace Statement
{
    public class BattleState : State
    {
        public GameObject Enemy;
        public DamageNumber DamageNumberView;
        public BattlePanel BattlePanel;
        public GameConfig GameConfig;
        private int _currency;

        public event Action<int> OnCurrencyChanged;

        public int Currency
        {
            get => _currency;
            private set
            {
                if (_currency == value) return;

                _currency = value;
                OnCurrencyChanged?.Invoke(_currency);
            }
        }

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

            BattlePanel.Init(this);

            Currency += GameConfig.StartCurrency;
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

        public void AddEntity(string key, int entity)
        {
            if (!string.IsNullOrEmpty(key) && !_entityMap.ContainsKey(key))
            { 
                _entityMap[key] = EcsHandler.PackEntity(entity);
            }
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
        public void AddCurrency(int amount)
        {
            if (amount <= 0) return;
            Currency += amount;
        }

        public bool TrySpendCurrency(int amount)
        {
            if (amount <= 0) return false;
            if (_currency < amount) return false;

            Currency -= amount;
            return true;
        }

        public void InvokeEntity(TowerBase tower)
        {
            EcsHandler.ThrowNewEvent<SpawnTowerEvent>(new SpawnTowerEvent(tower));
        }

        public void InvokeDamageView(Vector3 pos, float value)
        {
            DamageNumberView.Spawn(pos, value);
        }
    }
}