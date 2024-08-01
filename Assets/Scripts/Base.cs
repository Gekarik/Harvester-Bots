using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UnitCreator), typeof(Scanner))]
public class Base : MonoBehaviour
{
    [SerializeField] private Storage _storage;

    private Scanner _scanner;
    private UnitCreator _unitCreator;
    private List<Unit> _unitList;
    private Dictionary<Resource, Unit> _unitToResource = new Dictionary<Resource, Unit>();

    [field: SerializeField] public int MaxUnitCount { get; private set; }

    private void Awake()
    {
        _scanner = GetComponent<Scanner>();
        _unitCreator = GetComponent<UnitCreator>();
        _unitList = new List<Unit>(MaxUnitCount);
    }

    private void OnEnable()
    {
        _scanner.ResourceFounded += OnResourcesFound;
    }

    private void OnDisable()
    {
        _scanner.ResourceFounded -= OnResourcesFound;
    }

    private void Start()
    {
        for (int i = 0; i < MaxUnitCount; i++)
            _unitList.Add(_unitCreator.Create());
    }

    private void Update()
    {
        if (TryGetFreeUnit(out Unit freeUnit))
            _scanner.ScanForResources();
    }

    private void OnResourcesFound(List<Resource> resources)
    {
        foreach (var resource in resources)
        {
            if (_unitToResource.ContainsKey(resource) == false)
            {
                if (TryGetFreeUnit(out Unit unit) == true)
                    AssignUnit(unit, resource);
            }
        }
    }

    private void AssignUnit(Unit unit, Resource resource)
    {
        unit.OnTargetChange(resource);
        _unitToResource[resource] = unit;
    }

    private bool TryGetFreeUnit(out Unit freeUnit)
    {
        freeUnit = _unitList.Find(unit => unit.Status == Statuses.UnitStatuses.Free);
        return freeUnit != null;
    }
}
