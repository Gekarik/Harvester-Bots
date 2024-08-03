using System;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    [SerializeField] private float _scanRadius;
    [SerializeField] private LayerMask _resourceLayer;

    public event Action<List<Resource>> ResourceFounded;

    public void ScanForResources()
    {
        var foundedResources = new List<Resource>();
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, _scanRadius, _resourceLayer);

        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider.TryGetComponent(out Resource resource))
                foundedResources.Add(resource);

            ResourceFounded?.Invoke(foundedResources);
        }
    }
}
