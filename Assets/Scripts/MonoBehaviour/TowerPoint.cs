using UnityEngine;

public class TowerPoint : MonoBehaviour
{
    public Transform Holder;

    private void OnValidate()
    {
        Holder = transform;
    }
}
