using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class ResourceGrabber : MonoBehaviour
{
    [SerializeField] private Transform _holdPoint;
    public Resource TargetResource { get; private set; }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Resource resource) && resource == TargetResource)
            Grab(resource);
    }

    public void SetTargetResource(Resource resource)
    {
        TargetResource = resource;
    }

    public void ClearTarget()
    {
        TargetResource = null;
    }

    private void Grab(Resource resource)
    {
        resource.transform.SetParent(_holdPoint);
        resource.transform.SetPositionAndRotation(_holdPoint.position, _holdPoint.rotation);

        if (resource.TryGetComponent(out Rigidbody rigidbody))
            rigidbody.isKinematic = true;
    }
}
