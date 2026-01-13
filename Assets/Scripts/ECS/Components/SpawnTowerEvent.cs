namespace Client 
{
    struct SpawnTowerEvent 
    {
        public TowerBase Tower;

        public SpawnTowerEvent(TowerBase tower)
        {
            Tower = tower;
        }
    }
}
