using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Storage : MonoBehaviour
{
    public event Action<Resource> ResourceCollected;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Resource resource))
        {
            ResourceCollected?.Invoke(resource);
            resource.Collect();
        }
    }
}
