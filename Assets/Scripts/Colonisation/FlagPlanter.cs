using System;
using System.Collections.Generic;
using UnityEngine;

public class FlagPlanter : MonoBehaviour
{
    [SerializeField] private Flag _flagPrefab;

    private Base _base;
    private bool _isBaseClicked = false;
    private InputReader _inputReader = new();
    private Camera _mainCamera;
    private Dictionary<Base, Flag> _baseToFlag = new();

    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        if (_inputReader.LeftClick())
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
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
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
                    _base.SetFlag(flag);
                }
            }
        }

        _base = null;
        _isBaseClicked = false;
    }

    private void IsBaseClicked()
    {
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
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