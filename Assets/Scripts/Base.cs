using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UnitCreator), typeof(ScoreCounter), typeof(Scanner))]

public class Base : MonoBehaviour
{
    [SerializeField] private UnitCreator _unitCreator;
    [SerializeField] private ResourceManager _resourceManager;

    private List<Unit> _unitList;
    private Scanner _scanner;

    [field: SerializeField] public int MaxUnitCount { get; private set; }

    private void Awake()
    {
        _scanner = GetComponent<Scanner>();
        _unitList = new List<Unit>(MaxUnitCount);
    }

    private void Start()
    {
        InitializeUnits();
    }

    private void Update()
    {
        if (TryGetFreeUnit(out Unit unit))
            AssignResource(unit);
    }

    private void AssignResource(Unit unit)
    {
        var resources = _scanner.ScanResources();

        foreach (var resource in resources)
        {
            if (_resourceManager.TryAssignResource(unit, resource))
                unit.AssignResource(resource);
        }
    }

    private void InitializeUnits()
    {
        for (int i = 0; i < MaxUnitCount; i++)
            _unitList.Add(_unitCreator.Create());
    }

    private bool TryGetFreeUnit(out Unit freeUnit)
    {
        freeUnit = _unitList.Find(unit => unit.Status == Statuses.UnitStatuses.Free);
        return freeUnit != null;
    }
}
