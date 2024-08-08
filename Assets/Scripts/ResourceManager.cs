using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    private Dictionary<Resource, Unit> _unitToResource = new Dictionary<Resource, Unit>();

    public bool TryAssignResource(Unit unit, Resource resource)
    {
        if (_unitToResource.ContainsKey(resource))
            return false;

        _unitToResource[resource] = unit;
        return true;
    }


    public void RemoveResourceFromData(Resource resource)
    {
        if (_unitToResource.ContainsKey(resource))
            _unitToResource.Remove(resource);
    }
}