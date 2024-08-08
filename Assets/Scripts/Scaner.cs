using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    [SerializeField] private float _scanRadius;
    [SerializeField] private LayerMask _resourceLayer;

    public List<Resource> ScanResources()
    {
        var foundedResources = new List<Resource>();
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, _scanRadius, _resourceLayer);

        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider.TryGetComponent(out Resource resource))
                foundedResources.Add(resource);
        }

        return foundedResources;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _scanRadius);
    }
}
