using System.Collections.Generic;
using UnityEngine;

public class FlagPlanter : MonoBehaviour
{
    [SerializeField] private Flag _flagPrefab;

    private Base _selectedBase;
    private InputReader _inputReader = new InputReader();
    private Camera _mainCamera;
    private Dictionary<Base, Flag> _baseToFlag = new Dictionary<Base, Flag>();

    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        if (_inputReader.IsLeftButtonClicked())
            HandleClick();
    }

    private void OnFlagDestroy(Flag flag)
    {
        foreach (var pair in _baseToFlag)
        {
            if (pair.Value == flag)
            {
                flag.Destroyed -= OnFlagDestroy;
                _baseToFlag.Remove(pair.Key);
                break;
            }
        }
    }

    private void HandleBase(Base clickedBase)
    {
        _selectedBase = clickedBase;
        Debug.Log("База выбрана");
    }

    private void HandleClick()
    {
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
        {
            if (hit.collider.TryGetComponent(out Base clickedBase))
            {
                HandleBase(clickedBase);
            }
            else if (hit.collider.TryGetComponent(out PlayingField playingField))
            {
                TryPlantFlag(hit.point);
                _selectedBase = null;
            }
        }
    }

    private void TryPlantFlag(Vector3 hitPoint)
    {
        if (_selectedBase != null)
        {
            if (_baseToFlag.TryGetValue(_selectedBase, out Flag existingFlag))
            {
                existingFlag.transform.position = hitPoint;
            }
            else
            {
                var newFlag = Instantiate(_flagPrefab, hitPoint, Quaternion.identity);
                newFlag.Destroyed += OnFlagDestroy;
                _baseToFlag[_selectedBase] = newFlag;
            }
            _selectedBase.SetFlag(_baseToFlag[_selectedBase]);
        }
    }
}