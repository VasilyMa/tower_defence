using UnityEngine;

[CreateAssetMenu(fileName = "Tower", menuName = "Tower/NewTower")]
public class TowerBase : ScriptableObject
{
    public GameObject ViewObject;
    public GameObject ProjectileObject;
    public float Range;
    public float Damage;
    public float Speed;
    public float MissileSpeed;
    public float Rotation;
    public int Cost;

    public TowerType Type;
     
    public float Radius;
    public float Height;
}

public enum TowerType { archer, launcher }
