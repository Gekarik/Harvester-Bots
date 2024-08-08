using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UnitCreator), typeof(ScoreCounter), typeof(Scanner))]

public class Base : MonoBehaviour
{
    [SerializeField] private UnitCreator _unitCreator;

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
            case Statuses.BaseMode.SpawnUnits when _scoreCounter.Points >= 3:
                _scoreCounter.Remove(3);
                _unitList.Add(_unitCreator.Create());
                break;

            case Statuses.BaseMode.CreateNewBase when _scoreCounter.Points >= 5 && TryGetFreeUnit(out Unit builder):
                _scoreCounter.Remove(5);
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
        Debug.Log(resources.Count.ToString()); 

        foreach (var resource in resources)
        {
            if (ResourceManager.TryAssignResource(unit, resource))
            {
                unit.AssignResource(resource);
                return;

            }
            else
                Debug.Log("Can't Assign Resource");
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
        {
            Debug.Log($"Free unit found: {freeUnit}");
            return true;
        }
        Debug.Log("No free units found.");
        return false;
    }
}
