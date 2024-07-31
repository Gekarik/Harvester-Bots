using UnityEngine;

[RequireComponent(typeof(UnitManager),typeof(Scaner))]
public class Base : MonoBehaviour
{
    [SerializeField] private Storage _storage;

    private Scaner _scaner;
    private UnitManager _unitManager;

    private void OnEnable()
    {
        _scaner.OnFoundResource += NotifyUnits;
    }

    private void OnDisable()
    {
        _scaner.OnFoundResource -= NotifyUnits;
    }

    private void Awake()
    {
        _scaner = GetComponent<Scaner>();
        _unitManager = GetComponent<UnitManager>();
    }

    private void NotifyUnits(Resource resource)
    {
        _unitManager.AppointUnit(resource);
        resource.SetStatus(Statuses.ResourceStatuses.HasSender);
    }
}
