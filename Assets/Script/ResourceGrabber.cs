using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class ResourceGrabber : MonoBehaviour
{
    [SerializeField] private Transform _holdPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Resource resource) && resource.Status == Resource.Statuses.HasSender)
        {
            Grab(resource);
            resource.UpdateStatus(Resource.Statuses.Grabbed);
        }
    }

    private void Grab(Resource resource)
    {
        resource.transform.SetParent(_holdPoint);
        resource.transform.position = _holdPoint.position;
        resource.transform.rotation = _holdPoint.rotation;

        if (resource.TryGetComponent(out Rigidbody rb))
            rb.isKinematic = true;
    }
}
