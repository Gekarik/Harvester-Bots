using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UnitCreator), typeof(ScoreCounter), typeof(Scanner))]
public class Base : MonoBehaviour
{
    [SerializeField] private UnitCreator _unitCreator;

    private int _priceForUnit = 3;
    private int _priceForBase = 5;
    private List<Unit> _unitList;
    private Scanner _scanner;
    private Flag _currentFlag;
    private ScoreCounter _scoreCounter;
    private Statuses.BaseMode _mode;
    

    [field: SerializeField] public ResourceManager ResourceManager { get; private set; }
    [field: SerializeField] public int MaxUnitCount { get; private set; }

    private void Awake()
    {
        _scanner = GetComponent<Scanner>();
        _scoreCounter = GetComponent<ScoreCounter>();
        _unitList = new List<Unit>(MaxUnitCount);
    }

    private void Start()
    {
        InitializeUnits();
    }

    private void Update()
    {
        HandleBaseMode();
    }

    public void Init(ResourceManager resourceManager)
    {
        ResourceManager = resourceManager;
    }

    public void SetFlag(Flag flag)
    {
        _currentFlag = flag;
        _mode = Statuses.BaseMode.CreateNewBase;
    }

    private void HandleBaseMode()
    {
        switch (_mode)
        {
            case Statuses.BaseMode.SpawnUnits when _scoreCounter.Points >= _priceForUnit:
                _scoreCounter.Remove(_priceForUnit);
                _unitList.Add(_unitCreator.Create());
                break;

            case Statuses.BaseMode.CreateNewBase when _scoreCounter.Points >= _priceForBase && TryGetFreeUnit(out Unit builder):
                _scoreCounter.Remove(_priceForBase);
                CreateBase(builder);
                break;

            default:
                if (TryGetFreeUnit(out Unit searcher))
                    AssignResource(searcher);
                break;
        }
    }

    private void CreateBase(Unit unit)
    {
        unit.AssignBuilding(_currentFlag);
        _mode = Statuses.BaseMode.SpawnUnits;
        _unitList.Remove(unit);
    }

    private void AssignResource(Unit unit)
    {
        var resources = _scanner.ScanResources();

        foreach (var resource in resources)
        {
            if (ResourceManager.TryAssignResource(unit, resource))
            {
                unit.AssignResource(resource);
                return;
            }
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

        if (freeUnit != null)
            return true;

        return false;
    }
}
