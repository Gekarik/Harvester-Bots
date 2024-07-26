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

    private void Start()
    {
        _unitList = new List<Unit>();
        _home = GetComponent<Base>();
        SpawnUnits();
    }

    public void AppointUnit(Resource resource)
    {
        foreach (var unit in _unitList)
        {
            if (unit.Status == Unit.Statuses.Free)
            {
                resource.UpdateStatus(Resource.Statuses.HasSender);
                unit.OnTargetChange(resource.transform.position);
                return;
            }
        }
    }

    private Vector3 CalculateOffset()
    {
        var _random = new System.Random();
        var offsetX = _random.Next(0, 3);
        var offsetZ = _random.Next(0, 3);
        var offset = new Vector3(offsetX, 0, offsetZ);

        return offset;
    }

    private void SpawnUnits()
    {
        for (int i = 0; i < MaxUnitCount; i++)
        {
            var offset = CalculateOffset();
            var unit = Instantiate(_unitPrefab, _spawnPoint.position + offset, Quaternion.identity);

            unit.transform.SetParent(transform, true);
            unit.SetHome(_home);
            _unitList.Add(unit);
        }
    }
}
