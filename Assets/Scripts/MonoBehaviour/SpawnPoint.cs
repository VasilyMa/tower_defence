using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private float _radius; 

#if UNITY_EDITOR
    private void OnDrawGizmos()
    { 
        Gizmos.color = new Color(1f, 0.3f, 1f, 0.25f);
        Gizmos.DrawSphere(transform.position, _radius);

        Gizmos.color = new Color(1f, 0.1f, 1f, 1f);
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
#endif
}
