using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody),typeof(Collider))]
public class Resource : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private Collider _collider;
    public event Action<Resource> Collected;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
        _rigidbody = GetComponent<Rigidbody>();
        _collider.isTrigger = true;
        _rigidbody.useGravity = false;
    }

    public void Collect() => Collected?.Invoke(this);
}
