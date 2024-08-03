using System;
using UnityEngine;

public class Flag : MonoBehaviour
{
    public event Action<Flag> Destroyed;

    public void OnDestroy()
    {
        Destroyed?.Invoke(this);
    }
}
