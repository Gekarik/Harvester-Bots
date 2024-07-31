using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Storage : MonoBehaviour
{
    [SerializeField] private ResourcePool _pool;

    public event Action OnResourceCollected;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Resource resource))
        {
            _pool.PutObject(resource);
            resource.SetStatus(Statuses.ResourceStatuses.Free);
            OnResourceCollected?.Invoke();
        }
    }
}
