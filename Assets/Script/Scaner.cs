using System;
using System.Collections;
using UnityEngine;

public class Scaner : MonoBehaviour
{
    public Action<Resource> OnFoundResource;

    [SerializeField] private float _scanRadius = 50f;
    [SerializeField] private float _scanPeriod = 5f;
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
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, _scanRadius, _resourceLayer);

            foreach (Collider hitCollider in hitColliders)
            {
                if (hitCollider.TryGetComponent(out Resource resource) && resource.Status == Resource.Statuses.Free)
                    OnFoundResource?.Invoke(resource);
            }

            yield return wait;
        }
    }
}
