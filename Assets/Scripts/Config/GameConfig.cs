using UnityEngine;

[CreateAssetMenu(fileName = "GameConfig", menuName = "GameConfig")]
public class GameConfig : ScriptableObject
{
    public float SpawnTime;
    public int SpawnCount;
    public float Health;
    public float MoveSpeed;
    public int StartCurrency;
}
