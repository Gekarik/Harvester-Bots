using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Resource : MonoBehaviour
{
    public event Action<Resource> Collected;

    public void Collect() => Collected?.Invoke(this);
}
