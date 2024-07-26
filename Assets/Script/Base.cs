using UnityEngine;

[RequireComponent(typeof(UnitManager))]
public class Base : MonoBehaviour
{
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
    }
}
