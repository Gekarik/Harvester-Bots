using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UnitCreator), typeof(ScoreCounter))]

public class Base : MonoBehaviour
{
    [SerializeField] private UnitCreator _unitCreator;

    private List<Unit> _unitList;
    private Flag _currentFlag;
    private Statuses.BaseMode _mode;

    private Scanner _scanner;
    private FlagPlanter _flagPlanter;
    private ScoreCounter _scoreCounter;
    private ResourceManager _resourceManager;

    [field: SerializeField] public int MaxUnitCount { get; private set; }

    private void Awake()
    {
        _scanner = GetComponent<Scanner>();
        _flagPlanter = FindAnyObjectByType<FlagPlanter>();
        _scoreCounter = GetComponent<ScoreCounter>();
        _resourceManager = FindAnyObjectByType<ResourceManager>();
        _unitList = new List<Unit>(MaxUnitCount);
        _mode = Statuses.BaseMode.SpawnUnits;
    }

    private void OnEnable()
    {
        _scanner.ResourceFounded += OnResourcesFound;
        _flagPlanter.FlagChanged += OnFlagPlanted;
    }

    private void OnDisable()
    {
        _scanner.ResourceFounded -= OnResourcesFound;
        _flagPlanter.FlagChanged -= OnFlagPlanted;
    }

    private void Start()
    {
        InitializeUnits();
    }

    private void Update()
    {
        HandleBaseMode();
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

    private void HandleBaseMode()
    {
        switch (_mode)
        {
            case Statuses.BaseMode.SpawnUnits when _scoreCounter.Score >= 3:
                _scoreCounter.Remove(3);
                _unitList.Add(_unitCreator.Create());
                break;

            case Statuses.BaseMode.CreateNewBase when _scoreCounter.Score >= 5 && TryGetFreeUnit(out Unit unit):
                _scoreCounter.Remove(5);
                StartCoroutine(AssigntUnitToBaseBuilding(unit, _currentFlag));
                break;
        }
    }

    private void SearchResources()
    {
        if (TryGetFreeUnit(out Unit freeUnit))
            _scanner.ScanForResources();
    }

    private void OnFlagPlanted(Base _base, Flag flag)
    {
        if (this == _base)
        {
            _currentFlag = flag;
            _mode = Statuses.BaseMode.CreateNewBase;
        }
    }

    private void OnResourcesFound(List<Resource> resources)
    {
        foreach (var resource in resources)
        {
            if (TryGetFreeUnit(out Unit unit))
                AssigntUnitToResource(unit, resource);
        }
    }

    private IEnumerator AssigntUnitToBaseBuilding(Unit unit, Flag flag)
    {
        yield return StartCoroutine(unit.CreateBase(flag));
        Destroy(flag.gameObject);
        _mode = Statuses.BaseMode.SpawnUnits;
    }

    private bool TryGetFreeUnit(out Unit freeUnit)
    {
        freeUnit = _unitList.Find(unit => unit.Status == Statuses.UnitStatuses.Free);
        return freeUnit != null;
    }
}
