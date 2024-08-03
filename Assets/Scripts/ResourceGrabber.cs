using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class ResourceGrabber : MonoBehaviour
{
    [SerializeField] private Transform _holdPoint;
    private Resource _targetResource;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Resource resource) && resource == _targetResource)
            Grab(resource);
    }

    private void Grab(Resource resource)
    {
        resource.transform.SetParent(_holdPoint);
        resource.transform.SetPositionAndRotation(_holdPoint.position, _holdPoint.rotation);

        if (resource.TryGetComponent(out Rigidbody rigidbody))
            rigidbody.isKinematic = true;
    }

    public void SetTargetResource(Resource resource)
    {
        _targetResource = resource;
    }
}
