using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UnitCreator), typeof(ScoreCounter), typeof(Scanner))]

public class Base : MonoBehaviour
{
    [SerializeField] private UnitCreator _unitCreator;
    [SerializeField] private ResourceManager _resourceManager;

    private List<Unit> _unitList;
    private Statuses.BaseMode _mode;

    private Scanner _scanner;

    [field: SerializeField] public int MaxUnitCount { get; private set; }

    private void Awake()
    {
        _scanner = GetComponent<Scanner>();
        _unitList = new List<Unit>(MaxUnitCount);
        _mode = Statuses.BaseMode.SpawnUnits;
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
        InitializeUnits();
    }

    private void Update()
    {
        SearchResources();
    }

    public void AssigntUnitToResource(Unit unit, Resource resource)
    {
        if (_resourceManager.TryAssignResource(unit, resource))
            unit.OnTargetChange(resource);
    }

    private void InitializeUnits()
    {
        for (int i = 0; i < MaxUnitCount; i++)
            _unitList.Add(_unitCreator.Create());
    }

    private void SearchResources()
    {
        if (TryGetFreeUnit(out Unit freeUnit))
            _scanner.ScanForResources();
    }

    private void OnResourcesFound(List<Resource> resources)
    {
        foreach (var resource in resources)
        {
            if (TryGetFreeUnit(out Unit unit))
                AssigntUnitToResource(unit, resource);
        }
    }

    private bool TryGetFreeUnit(out Unit freeUnit)
    {
        freeUnit = _unitList.Find(unit => unit.Status == Statuses.UnitStatuses.Free);
        return freeUnit != null;
    }
}
