using System;
using System.Collections;
using UnityEngine;

public class Scaner : MonoBehaviour
{
    public event Action<Resource> OnFoundResource;

    [SerializeField] private float _scanRadius;
    [SerializeField] private float _scanPeriod;
    [SerializeField] private LayerMask _resourceLayer;

    private void Start()
    {
        StartCoroutine(nameof(ScanResources));
    }

    public IEnumerator ScanResources()
    {
        WaitForSeconds wait = new WaitForSeconds(_scanPeriod);

        while (true)
        {
            yield return wait;

            Collider[] hitColliders = Physics.OverlapSphere(transform.position, _scanRadius, _resourceLayer);

            foreach (Collider hitCollider in hitColliders)
            {
                if (hitCollider.TryGetComponent(out Resource resource) && resource.Status == Statuses.ResourceStatuses.Free)
                    OnFoundResource?.Invoke(resource);
            }
        }
    }
}
