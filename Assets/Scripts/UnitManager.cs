using UnityEngine;
using System;
using System.Collections.Generic;

public class UnitManager : MonoBehaviour
{
    [SerializeField] private Unit _unitPrefab;
    [SerializeField] private Transform _spawnPoint;
    [field: SerializeField] public int MaxUnitCount { get; private set; }

    private Base _home;
    private List<Unit> _unitList;
    private BoxCollider _spawnArea;

    private void Start()
    {
        _unitList = new List<Unit>();
        _home = GetComponent<Base>();
        _spawnArea = _spawnPoint.GetComponent<BoxCollider>();
        SpawnUnits();
    }

    public void AppointUnit(Resource resource)
    {
        foreach (var unit in _unitList)
        {
            if (unit.Status == Statuses.UnitStatuses.Free)
            {
                unit.OnTargetChange(resource);
                return;
            }
        }
    }

    private Vector3 CalculatePosition()
    {
        Bounds bounds = _spawnArea.bounds;

        return new Vector3(
            UnityEngine.Random.Range(bounds.min.x, bounds.max.x),
            UnityEngine.Random.Range(bounds.min.y, bounds.max.y),
            UnityEngine.Random.Range(bounds.min.z, bounds.max.z)
        );
    }

    private void SpawnUnits()
    {
        for (int i = 0; i < MaxUnitCount; i++)
        {
            var position = CalculatePosition();
            var unit = Instantiate(_unitPrefab, position, Quaternion.identity);

            unit.transform.SetParent(transform, true);
            unit.SetHome(_home);
            _unitList.Add(unit);
        }
    }
}
