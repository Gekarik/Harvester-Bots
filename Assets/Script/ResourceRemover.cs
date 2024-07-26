using System;
using UnityEngine;

public class ResourceRemover : MonoBehaviour
{
    public Action ResourceCollected;

    [SerializeField] private ResourcePool _pool;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Resource resource))
        {
            _pool.PutObject(resource);
            ResourceCollected?.Invoke();
        }
    }
}
