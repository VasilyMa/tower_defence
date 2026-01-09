using System;
using UnityEngine;

public class InjectionDestroyHook : MonoBehaviour
{
    public Action OnDestroyed;

    private void OnDestroy()
    {
        OnDestroyed?.Invoke();
    }
}
