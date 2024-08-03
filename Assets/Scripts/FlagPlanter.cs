using System;
using System.Collections.Generic;
using UnityEngine;

public class FlagPlanter : MonoBehaviour
{
    [SerializeField] private Flag _flagPrefab;

    public event Action<Base, Flag> FlagChanged;

    private Ray _ray;
    private Base _base;
    private bool _isBaseClicked = false;
    private Dictionary<Base, Flag> _baseToFlag = new();

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (_isBaseClicked == false)
                IsBaseClicked();
            else
                TryPlantFlag();
        }
    }

    private void OnFlagDestroy(Flag flag)
    {
        Base toDelete = null;

        foreach (var pair in _baseToFlag)
        {
            if (pair.Value == flag)
            {
                toDelete = pair.Key;
                break;
            }
        }

        if (toDelete != null)
        {
            flag.Destroyed -= OnFlagDestroy;
            _baseToFlag.Remove(toDelete);
        }
    }

    private void TryPlantFlag()
    {
        _ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(_ray, out RaycastHit hit, Mathf.Infinity))
        {
            if (hit.collider.TryGetComponent(out PlayingField playingField))
            {
                if (_baseToFlag.ContainsKey(_base))
                {
                    _baseToFlag[_base].transform.position = hit.point;
                }
                else
                {
                    var flag = Instantiate(_flagPrefab, hit.point, Quaternion.identity);
                    _baseToFlag[_base] = flag;
                    flag.Destroyed += OnFlagDestroy;
                    FlagChanged.Invoke(_base, flag);
                }
            }
        }

        _base = null;
        _isBaseClicked = false;
    }

    private void IsBaseClicked()
    {
        _ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(_ray, out RaycastHit hit, Mathf.Infinity))
        {
            if (hit.collider.TryGetComponent(out Base _base))
            {
                _isBaseClicked = true;
                this._base = _base;
                Debug.Log("База выбрана");
            }
            else
            {
                _isBaseClicked = false;
                Debug.Log("База невыбрана");
            }
        }
    }
}